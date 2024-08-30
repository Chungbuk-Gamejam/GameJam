using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObject : MonoBehaviour, IInteractable
{
    [Tooltip("아이템을 추가할 인벤토리")]
    [SerializeField] private InventoryManager inventoryManager;
    [Tooltip("아이템 이름")]
    [SerializeField] private string itemName;

    private void Start()
    {
        if(inventoryManager == null)
        {
            inventoryManager = FindFirstObjectByType<InventoryManager>();
        }
    }
    //인벤토리에 아이템을 추가한 후 필드에서 아이템 삭제
    public void Interact()
    {
        inventoryManager.AddItem(itemName);
        Destroy(gameObject);
    }
}
