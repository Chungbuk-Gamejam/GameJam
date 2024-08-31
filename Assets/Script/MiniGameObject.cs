using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MiniGameObject : MonoBehaviour
{
    Action<bool> _onCompleteGame;

    [SerializeField] GameObject _point;
    [SerializeField] float _speed;
    [SerializeField] float _moveLimit;
    [SerializeField] bool _isPlayGame;
    [SerializeField] bool _MoveRight;

    int _layerMask;

    private void Start()
    {
        _isPlayGame = true;
        _MoveRight = true;
        _layerMask = _layerMask = 1 << 7;
    }
    private void Update()
    {
        if (_isPlayGame)
        {
            MovePointer();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isPlayGame = false;

            if (CheckisSuccess())
                SuccessGame();
            else
                FailGame();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {   // Reset
            _isPlayGame = true;
        }
    }

    private void OnDisable()
    {
        if (_onCompleteGame != null)
        {
            _onCompleteGame = null;
        }
    }

    public void SetCompleteCallback(Action<bool> callback)
    {
        _onCompleteGame = callback;
    }

    public void SuccessGame()
    {
        _onCompleteGame?.Invoke(true);
        _onCompleteGame = null;
        Debug.Log($"SuccessGame");
    }

    public void FailGame()
    {
        _onCompleteGame?.Invoke(false);
        _onCompleteGame = null;
        Debug.Log($"FailGame");
    }

    public bool CheckisSuccess()
    {
        Vector3 vDown = Vector3.down;
        Vector3 vOrigin = Vector3.zero;
        if (_point != null)
            vOrigin = _point.GetComponent<RectTransform>().position;

        RaycastHit2D hit = Physics2D.Raycast(vOrigin, Vector3.down, 300);
        if (hit.collider != null)
            return true;

        return false;
    }

    public void MovePointer()
    {
        float pointPositionX = 0f;

        if (_point != null)
            pointPositionX = _point.GetComponent<RectTransform>().localPosition.x;

        if (_MoveRight == true && _moveLimit < pointPositionX)
            _MoveRight = false;

        if (_MoveRight == false && -_moveLimit > pointPositionX)
            _MoveRight = true;

        Vector2 dir = _MoveRight == true ? Vector2.right : Vector2.left;

        if (_point != null)
        {
            Vector3 P0 = _point.GetComponent<RectTransform>().localPosition;
            Vector3 AT = dir * _speed * Time.deltaTime;
            Vector3 P1 = P0 + AT;

            _point.GetComponent<RectTransform>().localPosition = P1;
            // 등속운동 P1 = PO + AT;
        }
    }

    private void OnDrawGizmos()
    {
        if(_point!= null )
        {
            Color color = Color.magenta;
            Vector2 origin = _point.GetComponent<RectTransform>().position;
            Vector2 to = new Vector2
                (_point.GetComponent<RectTransform>().position.x,
                _point.GetComponent<RectTransform>().position.y + (-1f * 300f));

            Gizmos.DrawLine(origin, to);
        }
    }
}
