using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static ItemInfo;

public class RecipeManager : MonoBehaviour
{
    static public RecipeManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 전환되어도 파괴되지 않음
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 있다면 새로 생성된 것을 파괴
        }
    }

    [SerializeField] public Recipe soupRecipe;
    [SerializeField] public Recipe recipe2;
    [SerializeField] public Recipe recipe3;
    [SerializeField] public Recipe recipe4;


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
