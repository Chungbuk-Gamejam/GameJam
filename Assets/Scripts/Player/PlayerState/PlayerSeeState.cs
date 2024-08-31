using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerSeeState : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;

    public void OnStateEnter(PlayerController npcController)
    {
        if (!_playerController)
            _playerController = npcController;

        _playerController.anim.SetBool("See", true);
        StartCoroutine(ChangeState());
        _playerController.telescope.SetActive(true);
    }
    public void OnStateUpdate()
    {
        
    }

    public void OnStateExit()
    {
        _playerController.anim.SetBool("See", false);
        _playerController.telescope.SetActive(false);
    }

    IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(6.0f);
        FadeManager.Instance.StartFadeIn();
        yield return new WaitForSeconds(2.0f);
        _playerController.ChangeState(_playerController._waitState);
    }
}
