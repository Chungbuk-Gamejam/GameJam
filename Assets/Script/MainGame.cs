using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(SceneLoader.GetInstance().AsyncSceneLoader($"MiniGame"));
            Debug.Log($"MiniGame Loading Start!!");
        }
    }
}
