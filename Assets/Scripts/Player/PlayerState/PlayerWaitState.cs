using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerWaitState : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;

    public void OnStateEnter(PlayerController npcController)
    {
        if (!_playerController)
            _playerController = npcController;

        _playerController.anim.SetBool("Wait", true);
    }
    public void OnStateUpdate()
    {
        
    }

    public void OnStateExit()
    {
        _playerController.anim.SetBool("Wait", false);
    }
}
