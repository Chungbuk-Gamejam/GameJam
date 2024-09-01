using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    public string RecipeName;

    // 스크립트에서 유니티 인스펙터에 노출하기 위해 임시로 사용되는 리스트
    [SerializeField]
    public List<ItemInfo.Item> ingredientItems = new List<ItemInfo.Item>();
    [SerializeField]
    public List<int> ingredientAmounts = new List<int>();


}
