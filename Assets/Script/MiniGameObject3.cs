using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MiniGameObject3 : MonoBehaviour
{
    public enum MiniGame3State
    {
        PointerMoveHorizontal,
        PointerMoveVertical,
        PointerMoveObsCollisionVertical 
    }

    public enum MiniGameState
    {
        Ready,
        InProgress,
        Success,
        Fail
    }

    Action<bool> _onCompleteGame;

    // lockpicker
    [SerializeField] Transform _lockpicker;
    [SerializeField] Vector3 _pickerShootRayPos;
    [SerializeField] MiniGame3State _eState = MiniGame3State.PointerMoveHorizontal;
    [SerializeField] MiniGameState _eCompleteState;



    [SerializeField] float _horizontalSpeed;
    [SerializeField] float _verticalSpeed;

    [SerializeField] float _moveHorizontalRightLimit;
    [SerializeField] float _moveHorizontalLeftLimit;

    [SerializeField] float _moveVerticalUpLimit;
    [SerializeField] float _moveVerticalDownLimit;

    [SerializeField] float _moveObsColliderVerticalUpLimit;
    [SerializeField] float _moveObsColliderVerticalDownLimit;

    [SerializeField] bool _moveRightHorizontal;
    [SerializeField] bool _moveUpVertical;

    //lockpin
    [SerializeField] List<MiniGame3_LockPinObject> _listLockPin = new List<MiniGame3_LockPinObject>();

    // bar
    [SerializeField] Transform _barEdge;
    [SerializeField] Transform _barInner;

    // Edge
    [SerializeField] Vector3 _vInitializeEdgePos;

    // Inner
    [SerializeField] Vector3 _vInitializeScale;

    [SerializeField] float _currentTime;
    [SerializeField] float _maxTime;

    [SerializeField] int _goalCount; // 성공 목표 갯수


    private void OnEnable()
    {
        Reset();
    }

    private void Update()
    {
        if(_eCompleteState == MiniGameState.Ready)
        {
            return;
        }

        _eCompleteState = CheckCompleteProgress();

        if (_eCompleteState == MiniGameState.Success)
        {
            SuccessGame();
        }
        else if (_eCompleteState == MiniGameState.Fail)
        {
            FailGame();
        }
        else
        {
            MiniGame3Input();
            ProgressMoveLockPicker();
            ProgressTimeBar();
        }
    }

    public void ChangeState()
    {
        _eCompleteState = MiniGameState.InProgress;
    }

    public void Reset()
    {
        _vInitializeEdgePos = _barEdge.position;

        _vInitializeScale = _barInner.localScale;
        _currentTime = _maxTime;

        _eState = MiniGame3State.PointerMoveHorizontal;
        _eCompleteState = MiniGameState.Ready;

        _moveRightHorizontal = true;
        _moveUpVertical = true;

        _verticalSpeed = UnityEngine.Random.Range(1, 10);
    }

    public void SetCompleteCallback(Action<bool> callback)
    {
        _onCompleteGame = callback;
    }

    public void SuccessGame()
    {
        _onCompleteGame?.Invoke(true);
        _onCompleteGame = null;
    }

    public void FailGame()
    {
        _onCompleteGame?.Invoke(false);
        _onCompleteGame = null;
    }

    public void MiniGame3Input()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(CheckRayObsCollider())
            {
                _eState = MiniGame3State.PointerMoveObsCollisionVertical;
            }
            else
            {
                _eState = MiniGame3State.PointerMoveVertical;
            }
        }
    }

    public bool CheckRayObsCollider()
    {
        RaycastHit2D[] hitinfos;
        hitinfos = Physics2D.RaycastAll(_lockpicker.position + _pickerShootRayPos, Vector2.up, float.MaxValue);
        
        for(int i = 0; i < hitinfos.Length; ++i)
        {
            var hitInfo = hitinfos[i];
            if (hitInfo.transform != null && hitInfo.transform.tag == $"MiniGame3_ObsCollider")
            {
                Debug.DrawRay(_lockpicker.position + _pickerShootRayPos, Vector2.up);
                return true;
            }
        }
        Debug.DrawRay(_lockpicker.position + _pickerShootRayPos, Vector2.up);
        return false;
    }

    public void ProgressMoveLockPicker()
    {
        switch (_eState)
        {
            case MiniGame3State.PointerMoveHorizontal:
                MovePointerHorizontal();
                break;
            case MiniGame3State.PointerMoveObsCollisionVertical:
                MovePointerObsVertical();
                break;
            case MiniGame3State.PointerMoveVertical:
                MovePointerVertical();
                break;
            default:
                break;
        }
    }

    public void MovePointerHorizontal()
    {
        float pointPositionX = 0f;
        // - 5.6 / -0.56

        if (_lockpicker != null)
            pointPositionX = _lockpicker.transform.position.x;

        if (_moveRightHorizontal == true && _moveHorizontalRightLimit < pointPositionX)
            _moveRightHorizontal = false;

        if (_moveRightHorizontal == false && _moveHorizontalLeftLimit > pointPositionX)
            _moveRightHorizontal = true;

        Vector2 dir = _moveRightHorizontal == true ? Vector2.right : Vector2.left;

        if (_lockpicker != null)
        {
            Vector3 P0 = _lockpicker.transform.position;
            Vector3 AT = dir * _horizontalSpeed * Time.deltaTime;
            Vector3 P1 = P0 + AT;

            _lockpicker.transform.position = P1;
            // 등속운동 P1 = PO + AT;
        }
    }

    public void MovePointerVertical()
    {
        float pointPositionY = 0f;
        // - 0.95  / -0.41

        if (_lockpicker != null)
            pointPositionY = _lockpicker.transform.position.y;

        if (_moveUpVertical == true && _moveVerticalUpLimit < pointPositionY)
            _moveUpVertical = false;

        if (_moveUpVertical == false && _moveVerticalDownLimit > pointPositionY)
        {
            _moveUpVertical = true;
            _eState = MiniGame3State.PointerMoveHorizontal;
        }

        Vector2 dir = _moveUpVertical == true ? Vector2.up : Vector2.down;

        if (_lockpicker != null)
        {
            Vector3 P0 = _lockpicker.transform.position;
            Vector3 AT = dir * _verticalSpeed * Time.deltaTime;
            Vector3 P1 = P0 + AT;

            _lockpicker.transform.position = P1;
            // 등속운동 P1 = PO + AT;
        }
    }

    public void MovePointerObsVertical()
    {
        float pointPositionY = 0f;
        // - 0.95  / -0.41

        if (_lockpicker != null)
            pointPositionY = _lockpicker.transform.position.y;

        if (_moveUpVertical == true && _moveObsColliderVerticalUpLimit < pointPositionY)
            _moveUpVertical = false;

        if (_moveUpVertical == false && _moveObsColliderVerticalDownLimit > pointPositionY)
        {
            _moveUpVertical = true;
            OrderPinHeightDown();
            _eState = MiniGame3State.PointerMoveHorizontal;
        }

        Vector2 dir = _moveUpVertical == true ? Vector2.up : Vector2.down;

        if (_lockpicker != null)
        {
            Vector3 P0 = _lockpicker.transform.position;
            Vector3 AT = dir * _verticalSpeed * Time.deltaTime;
            Vector3 P1 = P0 + AT;

            _lockpicker.transform.position = P1;
            // 등속운동 P1 = PO + AT;
        }
    }

    public void OrderPinHeightDown()
    {
        for(int i = 0; i < _listLockPin.Count; ++i)
        {
            var pinObject = _listLockPin[i];
            if (pinObject == null) continue;
            pinObject.SetHeightDown();
        }
    }

    public void ProgressTimeBar()
    {
        // HP 바의 위치를 대상 위로 설정
        _barEdge.position = _vInitializeEdgePos;

        DecreaseTime();

        // HP 비율 계산
        float _timeRatio = _currentTime / _maxTime;

        // 스케일 조정 (왼쪽에서 오른쪽으로 줄어드는 방식)
        _barInner.transform.localScale = new Vector3(_vInitializeScale.x * _timeRatio, _vInitializeScale.y, _vInitializeScale.z);
    }
    public MiniGameState CheckCompleteProgress()
    {
        // 남은 시간이 0 초 이상일때 진행
        int _count = _listLockPin.FindAll(rhs => rhs.Success == true).Count;
        if (_count >= _goalCount)
            return MiniGameState.Success;
        // Pin의 Success가 카운트 미만일 때 진행

        // true : 진행 , false : 종료
        if (_currentTime >= 0)
            return MiniGameState.InProgress;

        return MiniGameState.Fail;
        // 이외 모두 종료
    }

    public void DecreaseTime()
    {
        _currentTime = Mathf.Clamp(_currentTime -= Time.deltaTime, 0f, _maxTime);
    }

    private void OnDrawGizmos()
    {
        if (_lockpicker != null)
        {
            Color color = Color.magenta;
            Vector3 origin = _lockpicker.transform.position + _pickerShootRayPos;
            Vector3 to = _lockpicker.transform.position + _pickerShootRayPos + (Vector3.up * 15f);

            Gizmos.DrawLine(origin, to);
        }
    }


}




