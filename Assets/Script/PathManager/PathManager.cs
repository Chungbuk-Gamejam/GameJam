using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public enum Category
{
    Scene,
    Prefab
}

public class PathManager
{
    private bool _isInit;
    private Dictionary<Category, string> _pathDictionary = new Dictionary<Category, string>();

    private PathManager() 
    {
        if(_isInit == false)
        {
            Initialize();
            _isInit = true;
        }
    }
    public static PathManager Instance;
    public static PathManager GetInstance()
    {
        if(Instance == null)
            Instance = new PathManager();

         return Instance;
    }

    public void Initialize()
    {
        if (_pathDictionary == null) _pathDictionary = new Dictionary<Category, string  >();

        _pathDictionary.Add(Category.Scene, Application.dataPath + "/Scenes/");
        _pathDictionary.Add(Category.Prefab, Application.dataPath + "/Prefab/");

        // TODO : 파일 경로 필요할 때 추가.
    }

    public string GetScenePath(Category _enum)
    {
        string _path;
        _pathDictionary.TryGetValue(_enum, out _path);
        return _path;
    }
}












