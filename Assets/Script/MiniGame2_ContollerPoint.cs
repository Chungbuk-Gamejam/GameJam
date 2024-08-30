using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MiniGame2_ContollerPoint : MonoBehaviour
{
    [SerializeField] MiniGameObject2 _minigame2Object;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MiniGame2_Point")
        {
            ++_minigame2Object._currentSuccessCount;
            Debug.Log($"OnTriggerEnter2D :: {_minigame2Object._currentSuccessCount}");
        }
    }
}


