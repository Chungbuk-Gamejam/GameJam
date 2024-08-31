using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // 싱글톤 인스턴스

    // AudioClip을 저장할 변수들
    public AudioClip _backgroundMusicForTitle;
    public AudioClip _backgroundMusicForMainGame;
    public AudioClip _backgroundMusicForGoAurora;
    public AudioClip _backgroundMusicForGoConstellation;
    public AudioClip _backgroundMusicForGoMeteorShower;
    public AudioClip _backgroundMusicForGoMilkyWay;

    public AudioClip[] soundEffects;

    public AudioClip[] movesoundEffects;

    // AudioSource를 저장할 변수
    private AudioSource audioSource;

    public List<AudioSource> AudioSourceList = new List<AudioSource>();

    void Awake()
    {
        // 싱글톤 패턴 적용
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴 방지
        }
        else
        {
            Destroy(gameObject);
        }

        // AudioSource 컴포넌트 추가 및 설정
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // 배경 음악 재생 메서드
    public void PlayBackgroundMusicForTitle()
    {
        audioSource.clip = _backgroundMusicForTitle;
        audioSource.loop = true; // 반복 재생
        audioSource.volume = 0.3f; // 볼륨
        audioSource.Play();
    }

    public void TurnOffBackGroundMusicFor()
    {
        audioSource.Stop();
        audioSource.clip = null;
    }
    public void PlayBackgroundMusicForMainGame()
    {
        audioSource.clip = _backgroundMusicForMainGame;
        audioSource.loop = true; // 반복 재생
        audioSource.volume = 0.3f; // 볼륨
        audioSource.Play();
    }

    public void TurnOffBackGroundMusicForMainGame()
    {
        audioSource.Stop();
        audioSource.clip = null;
    }

    public void PlayBackgroundMusicForGoAurora()
    {
        audioSource.clip = _backgroundMusicForGoAurora;
        audioSource.loop = true; // 반복 재생
        audioSource.volume = 0.35f; // 볼륨
        audioSource.Play();
    }

    public void PlayBackgroundMusicForGoConstellation()
    {
        audioSource.clip = _backgroundMusicForGoConstellation;
        audioSource.loop = true; // 반복 재생
        audioSource.volume = 0.35f; // 볼륨
        audioSource.Play();
    }

    public void PlayBackgroundMusicForGoMeteorShower()
    {
        audioSource.clip = _backgroundMusicForGoMeteorShower;
        audioSource.loop = true; // 반복 재생
        audioSource.volume = 0.35f; // 볼륨
        audioSource.Play();
    }

    public void PlayBackgroundMusicForGoMilkyWay()
    {
        audioSource.clip = _backgroundMusicForGoMilkyWay;
        audioSource.loop = true; // 반복 재생
        audioSource.volume = 0.35f; // 볼륨
        audioSource.Play();
    }

    public void TurnOffBackGroundMusicForMainMenu()
    {
        audioSource.Stop();
        audioSource.clip = null;
    }

    // 특정 사운드 효과 재생 메서드
    public void PlaySoundEffect(int soundIndex)
    {
        if (soundIndex < 0 || soundIndex >= soundEffects.Length)
        {
            Debug.LogError("Invalid sound index.");
            return;
        }

        AudioSource audioSource = null;
        for(int i = 0; i < AudioSourceList.Count; ++i)
        {
            var Element = AudioSourceList[i];
            if (Element == null) continue;
            if (Element.isPlaying == true) continue;

            audioSource = AudioSourceList[i];
        }

        audioSource.PlayOneShot(soundEffects[soundIndex]);
    }

    public void PlaySoundMove()
    {
        int randomNumber = UnityEngine.Random.Range(0,4);

        AudioSource audioSource = null;
        for (int i = 0; i < AudioSourceList.Count; ++i)
        {
            var Element = AudioSourceList[i];
            if (Element == null) continue;
            if (Element.isPlaying == true) continue;

            audioSource = AudioSourceList[i];
        }

        audioSource.PlayOneShot(movesoundEffects[randomNumber]);
    }

    public void GetFreeAudioSource()
    {



    }

    // 기타 사운드 관리 관련 메서드들을 추가할 수 있음
}