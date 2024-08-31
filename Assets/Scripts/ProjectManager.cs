using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    public static ProjectManager Instance { get; private set; } // Singleton 인스턴스

    [SerializeField] private List<GameObject> fadeObject;
    [SerializeField] private GameObject map;

    private void OnEnable()
    {
        for (int i = 0; i < fadeObject.Count; i++)
        {
            fadeObject[i].SetActive(true);
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 전환되어도 파괴되지 않음
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 있다면 새로 생성된 것을 파괴
        }
    }

    public void UnLock()
    {
        if (map.activeSelf)
        {
            for (int i = 0; i < fadeObject.Count; i++)
            {
                fadeObject[i].SetActive(false);
            }
        }
    }

    public void Lock()
    {
        if (map.activeSelf)
        {
            for (int i = 0; i < fadeObject.Count; i++)
            {
                fadeObject[i].SetActive(true);
            }
        }
    }

}
