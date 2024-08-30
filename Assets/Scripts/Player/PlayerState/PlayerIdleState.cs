﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;

    public void OnStateEnter(PlayerController npcController)
    {
        if (!_playerController)
            _playerController = npcController;
    }
    public void OnStateUpdate()
    {
        //Debug.Log("Player Idle");
        // 방향키 입력시 Walking으로 상태 전이

        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            _playerController.ChangeState(_playerController._walkState);
        }

        if (Input.GetKeyDown(KeyCode.F)) // 'E' 키로 상호작용
        {
            _playerController.Interact();
        }

    }

    public void OnStateExit()
    {

    }

}
