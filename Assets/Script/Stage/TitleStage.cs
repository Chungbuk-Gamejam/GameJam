using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleStage : MonoBehaviour
{
    public void Start()
    {
        SoundManager.instance.PlayBackgroundMusicForTitle();
    }

}
