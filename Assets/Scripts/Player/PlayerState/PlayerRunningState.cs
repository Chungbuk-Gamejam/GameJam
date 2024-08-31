using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;
    private Vector2 firstInputDirection;

    public void OnStateEnter(PlayerController playerController)
    {
        if (!_playerController)
            _playerController = playerController;

        _playerController.anim.SetBool("Sprint", true);

        _playerController.movement = Vector2.zero;
        firstInputDirection = Vector2.zero;
    }


    public void OnStateUpdate()
    {
        if (_playerController)
        {
            _playerController.movement.x = Input.GetAxisRaw("Horizontal");
            _playerController.movement.y = Input.GetAxisRaw("Vertical");

            if (_playerController.movement.x > 0)
            {
                _playerController.anim.SetFloat("DirX", 1.0f);
                _playerController.anim.SetFloat("DirY", 0.0f);
            }
            else if (_playerController.movement.x < 0)
            {
                _playerController.anim.SetFloat("DirX", -1.0f);
                _playerController.anim.SetFloat("DirY", 0.0f);
            }
            else if (_playerController.movement.y < 0)
            {
                _playerController.anim.SetFloat("DirX", 0.0f);
                _playerController.anim.SetFloat("DirY", -1.0f);
            }
            else if (_playerController.movement.y > 0)
            {
                _playerController.anim.SetFloat("DirX", 0.0f);
                _playerController.anim.SetFloat("DirY", 1.0f);
            }

            if (_playerController.movement.x != 0 || _playerController.movement.y != 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    _playerController._rigidbody.MovePosition(_playerController._rigidbody.position + _playerController.runSpeed * Time.fixedDeltaTime * _playerController.movement.normalized);

                }
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    _playerController.ChangeState(_playerController._walkState);
                }
            }
            else
            {
                _playerController.ChangeState(_playerController._idleState);
            }

            if (Input.GetKeyDown(KeyCode.F)) // 'E' 키로 상호작용
            {
                _playerController.Interact();
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                _playerController._uiController.ControllInventory();
            }
        }
    }
    public void OnStateExit()
    {
        _playerController.anim.SetBool("Sprint", false);
        _playerController.movement = Vector2.zero;
    }
}