using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("인벤토리")]
    [Tooltip("인벤토리")]
    [SerializeField] public GameObject inventory;
    [Tooltip("플레이어 상태를 알기위한 컨트롤러")]
    [SerializeField] private PlayerController playerController;


    public void ControllInventory()
    {
        if (playerController.CurrentState != playerController._waitState)
        {
            if (!inventory.activeSelf)
            {
                inventory.SetActive(true);
            }
            else
            {
                inventory.SetActive(false);
            }
        }
    }

}
