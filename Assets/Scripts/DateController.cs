using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DateController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text text;

    private void Update()
    {
        text.text = playerController.dayCount.ToString() + "ÀÏÂ÷";
    }
}
