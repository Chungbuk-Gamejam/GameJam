using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMonoState : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;

    public void OnStateEnter(PlayerController playerController)
    {
        if (!_playerController)
            _playerController = playerController;
        _playerController.anim.SetBool("Monologue", true);
    }
    public void OnStateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _playerController.ChangeState(_playerController._idleState);
        }
    }

    public void OnStateExit()
    {
        _playerController.monologuePanel.SetActive(false);
        _playerController.anim.SetBool("Monologue", false);
    }
}
