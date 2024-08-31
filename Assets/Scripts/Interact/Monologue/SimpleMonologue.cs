using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMonologue : MonoBehaviour, IInteractable
{
    [Tooltip("독백 대사")]
    [SerializeField] private string monologue;
    private MonologueManager _monologueManager;

    void Start()
    {
        if(_monologueManager == null)
        {
            _monologueManager = FindObjectOfType<MonologueManager>();
        }
    }
    public void Interact()
    {
        _monologueManager.ShowMonologue(monologue);
    }
}
