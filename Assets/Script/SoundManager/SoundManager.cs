using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // �̱��� �ν��Ͻ�

    // AudioClip�� ������ ������
    public AudioClip _backgroundMusicForTitle;
    public AudioClip _backgroundMusicForMainGame;
    public AudioClip _backgroundMusicForGoAurora;
    public AudioClip _backgroundMusicForGoConstellation;
    public AudioClip _backgroundMusicForGoMeteorShower;
    public AudioClip _backgroundMusicForGoMilkyWay;

    public AudioClip[] soundEffects;

    public AudioClip[] movesoundEffects;

    // AudioSource�� ������ ����
    private AudioSource audioSource;

    public List<AudioSource> AudioSourceList = new List<AudioSource>();

    void Awake()
    {
        // �̱��� ���� ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı� ����
        }
        else
        {
            Destroy(gameObject);
        }

        // AudioSource ������Ʈ �߰� �� ����
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // ��� ���� ��� �޼���
    public void PlayBackgroundMusicForTitle()
    {
        audioSource.clip = _backgroundMusicForTitle;
        audioSource.loop = true; // �ݺ� ���
        audioSource.volume = 0.3f; // ����
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
        audioSource.loop = true; // �ݺ� ���
        audioSource.volume = 0.3f; // ����
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
        audioSource.loop = true; // �ݺ� ���
        audioSource.volume = 0.35f; // ����
        audioSource.Play();
    }

    public void PlayBackgroundMusicForGoConstellation()
    {
        audioSource.clip = _backgroundMusicForGoConstellation;
        audioSource.loop = true; // �ݺ� ���
        audioSource.volume = 0.35f; // ����
        audioSource.Play();
    }

    public void PlayBackgroundMusicForGoMeteorShower()
    {
        audioSource.clip = _backgroundMusicForGoMeteorShower;
        audioSource.loop = true; // �ݺ� ���
        audioSource.volume = 0.35f; // ����
        audioSource.Play();
    }

    public void PlayBackgroundMusicForGoMilkyWay()
    {
        audioSource.clip = _backgroundMusicForGoMilkyWay;
        audioSource.loop = true; // �ݺ� ���
        audioSource.volume = 0.35f; // ����
        audioSource.Play();
    }

    public void TurnOffBackGroundMusicForMainMenu()
    {
        audioSource.Stop();
        audioSource.clip = null;
    }

    // Ư�� ���� ȿ�� ��� �޼���
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

    // ��Ÿ ���� ���� ���� �޼������ �߰��� �� ����
}