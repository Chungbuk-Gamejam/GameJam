using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    public string RecipeName;
    public Dictionary<ItemInfo.Item, int> Ingredients = new Dictionary<ItemInfo.Item, int>();

    // 스크립트에서 유니티 인스펙터에 노출하기 위해 임시로 사용되는 리스트
    [SerializeField]
    private List<ItemInfo.Item> ingredientItems = new List<ItemInfo.Item>();
    [SerializeField]
    private List<int> ingredientAmounts = new List<int>();

    // 유니티 인스펙터에서 설정된 데이터를 Dictionary로 변환
    private void OnValidate()
    {
        Ingredients.Clear();
        for (int i = 0; i < ingredientItems.Count; i++)
        {
            if (!Ingredients.ContainsKey(ingredientItems[i]))
            {
                Ingredients.Add(ingredientItems[i], ingredientAmounts[i]);
            }
        }
    }
}
