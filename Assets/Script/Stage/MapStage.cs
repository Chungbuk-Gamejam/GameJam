using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStage : MonoBehaviour
{
    public void Start()
    {
        SoundManager.instance.TurnOffBackGroundMusicFor();
        SoundManager.instance.PlayBackgroundMusicForMainGame();
    }
}
