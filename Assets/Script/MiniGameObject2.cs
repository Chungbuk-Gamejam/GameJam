using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MiniGameObject2 : MonoBehaviour
{
    public enum MiniGameState
    {
        InProgress,
        Success,
        Fail
    }

    [SerializeField] MiniGameState _eCompleteState;

    Action<bool> _onCompleteGame;

    [SerializeField] RectTransform _point;
    [SerializeField] Vector3 _goalLine;

    [SerializeField] RectTransform _topLimitLine;
    [SerializeField] RectTransform _botLimitLine;

    [SerializeField] float _speed;

    [SerializeField] int _successedCount;
    [SerializeField] public int _currentSuccessCount;


    private void Start()
    {
        ResetGame();
    }
    private void Update()
    {
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
        _currentSuccessCount = 0;
        _eCompleteState = MiniGameState.InProgress;
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

    public MiniGameState CheckCompleteProgress()
    {
        // true : 진행 , false : 종료
        if(_point == null)
        {
            Debug.LogError($"[MiniGameObject2]CheckCompleteProgress _point is NULL");
            return MiniGameState.Fail;
        }

        float pointPosX = _point.position.x;
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

    public bool CheckisSuccess()
    {
        Vector3 vDown = Vector3.down;
        Vector3 vOrigin = Vector3.zero;
        if (_point != null)
            vOrigin = _point.transform.position;



        return false;
    }

    private void OnDrawGizmos()
    {
        Color color = Color.magenta;
        Vector3 to = _goalLine + Vector3.down * 5000f;
        Gizmos.DrawLine(_goalLine, to);

        color = Color.blue;
        to = _topLimitLine.position + Vector3.right * 5000f;
        Gizmos.DrawLine(_topLimitLine.position, to);

        to = _botLimitLine.position + Vector3.right * 5000f;
        Gizmos.DrawLine(_botLimitLine.position, to);
    }
}


