using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MiniGameObject2 : MonoBehaviour
{
    public enum MiniGameState
    {
        Ready,
        InProgress,
        Success,
        Fail,
        CloseTime,
    }

    [SerializeField] MiniGameState _eCompleteState;

    Action<bool> _onCompleteGame;

    [SerializeField] Vector3 _initializePointerPos;

    [SerializeField] RectTransform _point;
    [SerializeField] Animator _pointAnim;
    [SerializeField] Vector3 _goalLine;

    [SerializeField] RectTransform _topLimitLine;
    [SerializeField] RectTransform _botLimitLine;

    [SerializeField] float _speed;

    [SerializeField] int _successedCount;
    [SerializeField] public int _currentSuccessCount;

    Coroutine _closeCoroutine;
    [SerializeField] bool _isClosing;

    [SerializeField] PlayerController playerController;

    // anim
    [SerializeField] Animator[] _targetPointAnimator;

    // stamp_Success,Fail
    [SerializeField] GameObject[] _stampAnimator;

    private void OnEnable()
    {
        ResetGame();

        SoundManager.instance.TurnOffBackGroundMusic();
        SoundManager.instance.PlayBackgroundMusicForMiniGame();
    }
    private void Update()
    {
        if (_eCompleteState == MiniGameState.CloseTime ||
            _eCompleteState == MiniGameState.Ready)
            return;

        _eCompleteState = CheckCompleteProgress();

        if (_eCompleteState == MiniGameState.InProgress)
        {
            CheckOnPressFlyObject();
        }
        else
        {
            if (_eCompleteState == MiniGameState.Success)
                SuccessGame();
            else if (_eCompleteState == MiniGameState.Fail)
                FailGame();

            if (Input.GetKeyDown(KeyCode.R))
            {   // Reset
                ResetGame();
            }
        }
    }

    private void OnDisable()
    {
        if (_onCompleteGame != null)
        {
            _onCompleteGame = null;
        }
    }

    public void ChangeState()
    {
        _eCompleteState = MiniGameState.InProgress;
    }

    public void CheckOnPressFlyObject()
    {
        Vector2 dir = Vector2.zero;
        Vector2 _pointPosition = Vector2.zero;

        if (_point != null)
            _pointPosition = _point.localPosition;

        if (Input.GetKey(KeyCode.Space))
        {
            if (_pointPosition.y <= _topLimitLine.localPosition.y)
                dir = Vector2.right + Vector2.up;
            else
                dir = Vector2.right;

            dir = dir.normalized;
        }
        else
        {
            if (_pointPosition.y >= _botLimitLine.localPosition.y)
                dir = Vector2.right + Vector2.down;
            else
                dir = Vector2.right;

            dir = dir.normalized;
        }

        Vector2 P0 = _point.localPosition;
        Vector2 AT = dir * _speed * Time.deltaTime;

        Vector2 P1 = P0 + AT;
        _point.localPosition = P1;
    }

    public void ResetGame()
    {
        _point.localPosition = _initializePointerPos;
        _eCompleteState = MiniGameState.Ready;
        _currentSuccessCount = 0;
        _isClosing = false;
        _pointAnim.Play($"MiniGame3_Point_Ani");

        for(int i = 0; i < _targetPointAnimator.Length; ++i)
        {
            var _animator = _targetPointAnimator[i];
            if (_animator == null) continue;

            _animator.Play($"MiniGame3_TargetPoint_Idle");
        }

        for (int i = 0; i < _stampAnimator.Length; ++i)
        {
            var _animator = _stampAnimator[i];
            if (_animator == null) continue;

            _animator.SetActive(false);
        }
    }
    public void SetCompleteCallback(Action<bool> callback)
    {
        _onCompleteGame = callback;
    }

    public void SuccessGame()
    {
        if(_isClosing == false)
        {
            if (_closeCoroutine != null)
                _closeCoroutine = null;

            _closeCoroutine = StartCoroutine(CloseTime(2));

            if (_stampAnimator[0] != null)
                _stampAnimator[0].SetActive(true);

            _isClosing = true;
        }
    }

    public void FailGame()
    {
        if (_isClosing == false)
        {
            if (_closeCoroutine != null)
                _closeCoroutine = null;

            _closeCoroutine = StartCoroutine(CloseTime(2));

            if (_stampAnimator[1] != null)
                _stampAnimator[1].SetActive(true);

            _isClosing = true;
        }
    }

    public IEnumerator CloseTime(float _time)
    {
        yield return new WaitForSeconds(_time);

        if (_eCompleteState == MiniGameState.Success)
            _onCompleteGame?.Invoke(true);
        else if (_eCompleteState == MiniGameState.Fail)
            _onCompleteGame?.Invoke(false);
        else { }

        _onCompleteGame = null;
        if(playerController != null)
            playerController.ChangeState(playerController._idleState);
    }

    public MiniGameState CheckCompleteProgress()
    {
        // true : ÁøÇà , false : Á¾·á
        if(_point == null)
        {
            Debug.LogError($"[MiniGameObject2]CheckCompleteProgress _point is NULL");
            return MiniGameState.Fail;
        }

        float pointPosX = _point.localPosition.x;
        if (pointPosX > _goalLine.x)
        {
            if (_successedCount <= _currentSuccessCount)
                return MiniGameState.Success;
            else
                return MiniGameState.Fail;
        }

        if (_successedCount <= _currentSuccessCount)
            return MiniGameState.Success;

        return MiniGameState.InProgress;
    }
    private void OnDrawGizmos()
    {
        Color color = Color.magenta;
        Vector3 origin = _goalLine + Vector3.right * 640f;
        Vector3 to =   _goalLine + Vector3.down * 5000f + Vector3.right * 640f;
        Gizmos.DrawLine(origin, to);

        color = Color.blue;
        to = _topLimitLine.position + Vector3.right * 2000f;
        Gizmos.DrawLine(_topLimitLine.position, to);

        to = _botLimitLine.position + Vector3.right * 2000f;
        Gizmos.DrawLine(_botLimitLine.position, to);
    }
}


