using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // �̱��� �ν��Ͻ�

    // AudioClip�� ������ ������
    public AudioClip backgroundMusicForCutScene;

    public AudioClip backgroundMusicForMainGame;

    public AudioClip backgroundMusicForMainMenu;

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
    public void PlayBackgroundMusicForCutScene()
    {
        audioSource.clip = backgroundMusicForCutScene;
        audioSource.loop = true; // �ݺ� ���
        audioSource.volume = 0.3f; // ����
        audioSource.Play();
    }

    public void TurnOffBackGroundMusicForCutScene()
    {
        audioSource.Stop();
        audioSource.clip = null;
    }
    public void PlayBackgroundMusicForMainGame()
    {
        audioSource.clip = backgroundMusicForMainGame;
        audioSource.loop = true; // �ݺ� ���
        audioSource.volume = 0.3f; // ����
        audioSource.Play();
    }

    public void TurnOffBackGroundMusicForMainGame()
    {
        audioSource.Stop();
        audioSource.clip = null;
    }

    public void PlayBackgroundMusicForMainMenu()
    {
        audioSource.clip = backgroundMusicForMainMenu;
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