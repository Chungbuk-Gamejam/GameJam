using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<string> inventory = new List<string>();

    private Dictionary<int, int> inventor = new Dictionary<int, int>();

    public void AddItem(string itemName)
    {
        Debug.Log(itemName + "이(가) 인벤토리에 추가되었습니다.");
        inventory.Add(itemName);
    }

    public void RemoveItem(string itemName)
    {
        Debug.Log(itemName + "이(가) 인벤토리에 제거되었습니다.");
        inventory.Remove(itemName);
    }

    public bool HasItem(string itemName)
    {
        Debug.Log(itemName + "을(를) 소지하고 있는지 확인합니다.");
        return inventory.Contains(itemName);
    }
}
