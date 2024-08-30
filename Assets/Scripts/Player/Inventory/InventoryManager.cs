using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemInfo;

public class InventoryManager : MonoBehaviour
{
    private Dictionary<Item, int> inventory = new Dictionary<Item, int>();

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

        // for문을 사용하여 모든 아이템 정보 출력
        for (int i = 0; i < keys.Count; i++)
        {
            Debug.Log("Item: " + keys[i] + ", Quantity: " + values[i]);
        }
    }
}
