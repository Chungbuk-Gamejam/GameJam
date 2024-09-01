using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStage : MonoBehaviour
{
    PlayerController _player;
    public void Start()
    {
        SoundManager.instance.TurnOffBackGroundMusic();
        SoundManager.instance.PlayBackgroundMusicForMainGame();
    }

    public void Update()
    {
        if(_player != null)
        {

        }
    }




}
