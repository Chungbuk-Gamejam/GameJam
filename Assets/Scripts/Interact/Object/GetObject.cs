using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemInfo;

public class GetObject : MonoBehaviour, IInteractable
{
    public enum ItemType
    {
        Vegetable,
        Meat,
        Fish,
        Spice
    }
    [Tooltip("아이템을 추가할 인벤토리")]
    [SerializeField] private InventoryManager inventoryManager;
    [Tooltip("아이템 이름")]
    [SerializeField] private ItemType itemTypeInfo;
    [Tooltip("회전 시킬 오브젝트")]
    [SerializeField] private RectTransform rectTransform;
    [Tooltip("카운터를 적용할 플레이어 컨트롤러")]
    [SerializeField] private PlayerController playerController;


    private void Start()
    {
        if(inventoryManager == null)
        {
            inventoryManager = FindFirstObjectByType<InventoryManager>();
        }

        if(rectTransform == null)
        {
            rectTransform = GameObject.FindWithTag("Clock").GetComponent<RectTransform>();
        }

        if(playerController == null)
        {
            playerController = FindFirstObjectByType<PlayerController>();
        }
    }
    //인벤토리에 아이템을 추가한 후 필드에서 아이템 삭제
    //상호작용 카운터 1 감소
    public void Interact()
    {
        RotateClock();
        playerController.interactCounter--;
        switch (itemTypeInfo)
        {
            case ItemType.Vegetable:
                switch (Random.Range(0, 7))
                {
                    case 0:
                        inventoryManager.AddItem(Item.Garlic);
                        break;
                    case 1:
                        inventoryManager.AddItem(Item.GreenOnion);
                        break;
                    case 2:
                        inventoryManager.AddItem(Item.Onion);
                        break;
                    case 3:
                        inventoryManager.AddItem(Item.Mushroom);
                        break;
                    case 4:
                        inventoryManager.AddItem(Item.Potato);
                        break;
                    case 5:
                        inventoryManager.AddItem(Item.Radish);
                        break;
                }
                break;
            case ItemType.Spice:
                switch (Random.Range(0, 2))
                {
                    case 0:
                        inventoryManager.AddItem(Item.Soy);
                        break;
                    case 1:
                        inventoryManager.AddItem(Item.ChiliPepper);
                        break;
                }
                break;
            case ItemType.Meat:
                switch (Random.Range(0, 1))
                {
                    case 0:
                        inventoryManager.AddItem(Item.Meat);
                        break;
                }
                break;
            case ItemType.Fish:
                switch (Random.Range(0, 1))
                {
                    case 0:
                        inventoryManager.AddItem(Item.Fish);
                        break;
                }
                break;
        }
        Destroy(gameObject);
    }

        public void RotateClock()
    {
        rectTransform.localEulerAngles = new Vector3(
                rectTransform.localEulerAngles.x,
                rectTransform.localEulerAngles.y,
                rectTransform.localEulerAngles.z - 15f
            );
    }
}
