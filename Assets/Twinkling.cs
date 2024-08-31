using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Twinkling : MonoBehaviour
{
    private static bool IsTwinkling;
    private float ZeroToOne;
    private bool IsIncreasing;
    private float twinklingSpeed = 2.0f;
    private TextMeshProUGUI myTMP;

    public static void ChangeTwinkling()
    {
        IsTwinkling = false;
    }
    private void Start()
    {
        ZeroToOne = 0;
        IsTwinkling = true;
        IsIncreasing = true;

        myTMP = GetComponent<TextMeshProUGUI>();
    }   
    private void FixedUpdate()
    {
        if (IsTwinkling)
        {
            float tmpTime = Time.deltaTime;
            ZeroToOne += (tmpTime - (float)Math.Floor(tmpTime)) / twinklingSpeed * (IsIncreasing ? 1 : -1);
            if (ZeroToOne > 1)
            {
                IsIncreasing = false;
            }
            if (ZeroToOne < 0)
            {
                IsIncreasing = true;
            }
        }
        else
        {
            ZeroToOne = 1;
        }
        Color colorTmp = myTMP.color;
        colorTmp.a = ZeroToOne;
        myTMP.color = colorTmp;
    }
}
