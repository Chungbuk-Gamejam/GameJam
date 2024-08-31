using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MiniGame3_LockPinObject : MonoBehaviour
{
    [SerializeField] MiniGameObject3 _minigame3Object;

    [SerializeField] GameObject _objSuccess;
    [SerializeField] GameObject _objFail;

    [SerializeField] float _InitHeight;
    [SerializeField] float _pinHeightDown;

    bool _isSuccess;
    public bool Success => _isSuccess;

    public void Start()
    {
        _InitHeight = this.transform.position.y;
        _isSuccess = false;
    }
    public void OnEnable()
    {
        OnUpdate(); 
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MiniGame3_PinCollider")
        {
            SetIsUpper(true);
        }
        OnUpdate();
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "MiniGame3_PinCollider")
        {
            SetIsUpper(false);
        }
        OnUpdate();
    }

    public void OnUpdate()
    {
        // UI Update
        _objSuccess.SetActive(this._isSuccess);
        _objFail.SetActive(!this._isSuccess);
    }
    public void SetIsUpper(bool _isSuccess)
    {
        this._isSuccess = _isSuccess;
    }

    public void SetHeightDown()
    {
        Vector3 pos = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y - _pinHeightDown, _InitHeight, float.MaxValue), transform.position.z);
        transform.position = pos;
    }
}




