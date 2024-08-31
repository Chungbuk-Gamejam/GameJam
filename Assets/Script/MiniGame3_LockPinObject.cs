using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MiniGame3_LockPinObject : MonoBehaviour
{
    [SerializeField] MiniGameObject3 _minigame3Object;

    [SerializeField] GameObject _objSuccess;
    [SerializeField] GameObject _objFail;

    float _InitHeight;
    [SerializeField] float _pinHeightDown;

    bool _isSuccess;
    public bool Success => _isSuccess;
    public void Awake()
    {
        _InitHeight = 0.72f;
    }

    public void OnEnable()
    {
        _isSuccess = false;
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, _InitHeight,0f);
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
        Vector3 pos = new Vector3(transform.localPosition.x, Mathf.Clamp(transform.localPosition.y - _pinHeightDown, _InitHeight, float.MaxValue), transform.localPosition.z);
        transform.localPosition = pos;
    }
}




