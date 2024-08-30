using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemInfo;

public class GetObject : MonoBehaviour, IInteractable
{
    [Tooltip("아이템을 추가할 인벤토리")]
    [SerializeField] private InventoryManager inventoryManager;
    [Tooltip("아이템 이름")]
    [SerializeField] private Item itemInfo;
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
        inventoryManager.AddItem(itemInfo);
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
