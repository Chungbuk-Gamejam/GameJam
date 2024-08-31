using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MiniGameObject3 : MonoBehaviour
{
    Action _onCompleteGame;

    [SerializeField] RectTransform _point;
    [SerializeField] Vector3 _goalLine;

    [SerializeField] float _speed;
    [SerializeField] bool _isPlayGame;

    [SerializeField] int _successedCount;
    [SerializeField] public int _currentSuccessCount;


    private void Start()
    {
    }
}


