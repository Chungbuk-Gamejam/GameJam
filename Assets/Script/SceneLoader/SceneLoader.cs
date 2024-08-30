using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SceneLoader 
{
    private SceneLoader() { }
    public static SceneLoader Instance;
    public static SceneLoader GetInstance()
    {
        if(Instance == null)
            Instance = new SceneLoader();

         return Instance;
    }
    public IEnumerator AsyncSceneLoader(string _sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Debug.Log($"{_sceneName} Complete!!");
    }
}












