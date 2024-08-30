using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    [SerializeField] public Recipe soupRecipe;

    void Start()
    {
        if (soupRecipe != null)
        {
            Debug.Log("Recipe: " + soupRecipe.RecipeName);

            var keys = new List<ItemInfo.Item>(soupRecipe.Ingredients.Keys);
            var values = new List<int>(soupRecipe.Ingredients.Values);

            for (int i = 0; i < keys.Count; i++)
            {
                Debug.Log(keys[i] + " : " + values[i]);
            }
        }
    }
}
