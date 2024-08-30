using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Tooltip("인벤토리")]
    [SerializeField] private GameObject inventory;

    public void ControllInventory()
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
