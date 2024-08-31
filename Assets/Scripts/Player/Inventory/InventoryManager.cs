using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ItemInfo;


public class InventoryManager : MonoBehaviour
{
    private Dictionary<Item, int> inventory = new Dictionary<Item, int>();

    [Tooltip("요리하기에서 보여줄 프리팹")]
    [SerializeField] private GameObject slotPrefab; // 슬롯 프리팹
    [Tooltip("GridLayoutGroup이 있는 부모 오브젝트")]
    [SerializeField] private Transform slotGridParent; // GridLayoutGroup이 있는 부모 오브젝트
    [Tooltip("슬롯 패널")]
    [SerializeField] private GameObject slotPanel;
    [Tooltip("각 아이템에 해당하는 이미지 스프라이트 배열")]
    [SerializeField] private Sprite[] itemSprites; // 각 아이템에 해당하는 이미지 스프라이트 배열
    [Space(10)]

    [Tooltip("인벤토리에서 보여줄 프리팹")]
    [SerializeField] private GameObject invenPrefab;
    [Tooltip("GridLayoutGroup이 있는 부모 오브젝트")]
    [SerializeField] private Transform invenGridParent; // GridLayoutGroup이 있는 부모 오브젝트
    [Header("스탬프")]
    [SerializeField] private Sprite badStamp;
    [SerializeField] private Sprite goodStamp;
    [Header("결과")]
    [SerializeField] private Image result;

    private void Start()
    {
        //CreateRecipeSlots(RecipeManager.instance.mushroomSoup);
        //AddItem(Item.Meat);
    }

    public void AddItem(Item itemName)
    {
        if (inventory.ContainsKey(itemName))
        {
            Debug.Log(itemName + "이(가) 인벤토리에 추가되었습니다.");
            inventory[itemName] += 1;
            PrintInventory();
        }
        else
        {
            Debug.Log(itemName + "을 처음으로 습득했습니다.");
            inventory[itemName] = 1; // 처음 추가될 때 0으로 초기화 후 1을 더함
            PrintInventory();
        }

    }

    public void RemoveItem(Item itemName)
    {
        if (inventory.ContainsKey(itemName))
        {
            Debug.Log(itemName + "이(가) 인벤토리에서 1개 제거되었습니다.");
            inventory[itemName] -= 1;
            PrintInventory();

            // 아이템 수가 0이 되면 딕셔너리에서 제거
            if (inventory[itemName] <= 0)
            {
                inventory.Remove(itemName);
            }
        }
    }

    public bool HasItem(Item itemName)
    {
        if (inventory.ContainsKey(itemName))
        {
            PrintInventory();
            Debug.Log(itemName + "을(를) 소지하고 있는지 확인합니다.");
            return true;
        }
        return false;
    }

    //현재 인벤토리에 어떤 아이템들이 있는지 출력
    public void PrintInventory()
    {
        // 딕셔너리의 키와 값을 각각 리스트로 변환
        List<Item> keys = new List<Item>(inventory.Keys);
        List<int> values = new List<int>(inventory.Values);

        // 자식 오브젝트들을 순회하면서 제거
        for (int i = invenGridParent.childCount - 1; i >= 0; i--)
        {
            GameObject child = invenGridParent.GetChild(i).gameObject;
            Destroy(child);
        }

        for (int i = 0; i < keys.Count; i++)
        {
            Item item = keys[i];
            int requiredQuantity = values[i];

            // 슬롯을 인스턴스화
            GameObject inven = Instantiate(invenPrefab, invenGridParent);

            // Image 컴포넌트 설정
            Image itemImage = inven.transform.Find("Image").GetComponent<Image>();
            itemImage.sprite = GetItemSprite(item); // 아이템에 맞는 스프라이트 할당

            // Count 텍스트 설정
            TextMeshProUGUI countText = inven.transform.Find("Count").GetComponent<TextMeshProUGUI>();
            countText.text = requiredQuantity.ToString() + "개";
        }
    }

    // 레시피에 필요한 모든 재료가 인벤토리에 있는지 확인하는 메서드
    public string CanCraftRecipe(Recipe recipe)
    {
        // 레시피의 재료를 리스트로 변환
        List<Item> keys = new List<Item>(recipe.Ingredients.Keys);
        List<int> values = new List<int>(recipe.Ingredients.Values);

        bool hasAllItems = true;
        bool hasPartialItems = false;

        for (int i = 0; i < keys.Count; i++)
        {
            Item item = keys[i];
            int requiredQuantity = values[i];
            int currentQuantity = inventory.ContainsKey(item) ? inventory[item] : 0;

            // 현재 보유 수량과 필요한 수량을 출력
            Debug.Log(item + ": 현재 수량 = " + currentQuantity + ", 필요한 수량 = " + requiredQuantity);

            if (currentQuantity == 0)
            {
                // 하나라도 없는 재료가 있다면 "실패"
                hasAllItems = false;
            }
            else if (currentQuantity < requiredQuantity)
            {
                // 재료는 있지만 필요한 수량보다 적은 경우
                hasAllItems = false;
                hasPartialItems = true;
            }
            else
            {
                // 재료가 충분한 경우
                hasPartialItems = true;
            }
        }

        if (hasAllItems)
        {
            Debug.Log("레시피를 만드는데 필요한 모든 재료가 충분히 있습니다.");
            return "성공";
        }
        else if (hasPartialItems)
        {
            Debug.Log("레시피를 만드는데 필요한 재료들이 일부 부족하지만, 몇 가지 재료는 보유하고 있습니다.");
            return "부분성공";
        }
        else
        {
            Debug.Log("레시피를 만드는데 필요한 재료 중 하나 이상이 부족합니다.");
            return "실패";
        }
    }

    public void CreateRecipeSlots(Recipe recipe)
    {
        slotPanel.SetActive(true);
        // 레시피의 재료를 리스트로 변환
        List<Item> keys = new List<Item>(recipe.Ingredients.Keys);
        List<int> values = new List<int>(recipe.Ingredients.Values);

        bool hasAllItems = true;
        bool hasPartialItems = false;

        for (int i = 0; i < keys.Count; i++)
        {
            Item item = keys[i];
            int requiredQuantity = values[i];
            int currentQuantity = inventory.ContainsKey(item) ? inventory[item] : 0;

            // 슬롯을 인스턴스화
            GameObject slot = Instantiate(slotPrefab, slotGridParent);

            // Image 컴포넌트 설정
            Image itemImage = slot.transform.Find("Image").GetComponent<Image>();
            itemImage.sprite = GetItemSprite(item); // 아이템에 맞는 스프라이트 할당

            Image resultImage = slot.transform.Find("Result").GetComponent<Image>();

            if (currentQuantity >= requiredQuantity)
            {
                resultImage.sprite = goodStamp;
            }
            else
            {
                resultImage.sprite = badStamp;
            }

            if (currentQuantity == 0)
            {
                // 하나라도 없는 재료가 있다면 "실패"
                hasAllItems = false;
            }
            else if (currentQuantity < requiredQuantity)
            {
                // 재료는 있지만 필요한 수량보다 적은 경우
                hasAllItems = false;
                hasPartialItems = true;
            }
            else
            {
                // 재료가 충분한 경우
                hasPartialItems = true;
            }
            // Count 텍스트 설정
            TextMeshProUGUI countText = slot.transform.Find("Count").GetComponent<TextMeshProUGUI>();
            countText.text = currentQuantity + "/" + requiredQuantity;
        }

        if (hasAllItems)
        {
            Debug.Log("레시피를 만드는데 필요한 모든 재료가 충분히 있습니다.");
            result.sprite = goodStamp;
        }
        else
        {
            Debug.Log("레시피를 만드는데 필요한 재료 중 하나 이상이 부족합니다.");
            result.sprite = badStamp;
        }
    }

    // 아이템에 맞는 스프라이트를 반환하는 메서드
    private Sprite GetItemSprite(Item item)
    {
        // itemSprites 배열에 스프라이트가 각 아이템에 맞게 순서대로 배치되었다고 가정
        // 예를 들어, Carrot은 itemSprites[0], Potato는 itemSprites[1], Tomato는 itemSprites[2] 등에 해당한다고 가정

        switch (item)
        {
            case Item.Soy:
                return itemSprites[0];
            case Item.Radish:
                return itemSprites[1];
            case Item.Fish:
                return itemSprites[2];
            case Item.Salt:
                return itemSprites[3];
            case Item.Sugar:
                return itemSprites[4];
            case Item.Garlic:
                return itemSprites[5];
            case Item.Meat:
                return itemSprites[6];
            case Item.Potato:
                return itemSprites[7];
            case Item.ChiliPepper:
                return itemSprites[8];
            case Item.Mushroom:
                return itemSprites[9];
            case Item.GreenOnion:
                return itemSprites[10];
            default:
        
                return null; // 아이템에 맞는 스프라이트가 없을 경우 null 반환
        }
    }
}
