using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FishingBackground : MonoBehaviour
{
    public Image img;
    public Sprite[] sprites;
    private int index=0;
    public static bool IsActive=false;
    public int reverseSpeed;
    private void Update()
    {
        if (!IsActive)
        {
            return;
        }
        img.sprite = sprites[index/reverseSpeed];
        index++;
        if(index==reverseSpeed*sprites.Length)index = 0;
    }

}
