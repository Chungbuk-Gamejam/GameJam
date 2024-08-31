using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance { get; private set; } // Singleton �ν��Ͻ�

    public Image fadeImage; // ���̵� ȿ���� ���� Image
    public float fadeDuration = 1f; // ���̵� ��/�ƿ� ���� �ð�
    public float delayBeforeFadeOut = 1f; // ���̵� �� �� ���̵� �ƿ������� ��� �ð�
    private WaitForSeconds oneSecondWait = new WaitForSeconds(1.0f);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� ��ȯ�Ǿ �ı����� ����
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �ִٸ� ���� ������ ���� �ı�
        }
    }

    void Start()
    {
        JustFadeOut();
        //fadeImage.gameObject.SetActive(false);
    }

    public void JustFade() //�ٷ� Fade in �Ǵ� ȿ��
    {
        fadeImage.gameObject.SetActive(true);
        Color color = fadeImage.color;
        color.a = 1;
        fadeImage.color = color;
    }

    public void JustFadeOut() //�ٷ� Fade out �Ǵ� ȿ��
    {
        fadeImage.gameObject.SetActive(true);
        Color color = fadeImage.color;
        color.a = 0;
        fadeImage.color = color;
    }

    public void StartFade()
    {
        StartCoroutine(FadeInAndOut());
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        // ���̵� �� ����
        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(0, 1, false)); // ���̵� �� �Ϸ� ���
    }

    private IEnumerator FadeOut()
    {
        // ���̵� �ƿ� ����
        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1, 0, true)); // ���̵� �ƿ� �Ϸ� ���
        fadeImage.gameObject.SetActive(false);
    }

    /* startAlpha : ���� ����
     * endAlpha : �� ����
     * deactivateOnEnd : ������ ������ ��Ȱ��ȭ�� ����
     */
    private IEnumerator Fade(float startAlpha, float endAlpha, bool deactivateOnEnd)
    {
        fadeImage.gameObject.SetActive(true);

        Color color = fadeImage.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = endAlpha;
        fadeImage.color = color;

        if (deactivateOnEnd)
        {
            fadeImage.gameObject.SetActive(false); // ���������� ��Ȱ��ȭ
        }

    }

    private IEnumerator FadeInAndOut()
    {
        yield return oneSecondWait; // 1�� ���
        yield return StartCoroutine(FadeIn()); // ���̵� �� ȿ�� ���� �� �Ϸ� ���
        yield return new WaitForSeconds(delayBeforeFadeOut); // �߰� ��� �ð�
        yield return StartCoroutine(FadeOut()); // ���̵� �ƿ� ȿ�� ���� �� �Ϸ� ���
        yield return oneSecondWait; // 1�� ���
    }
}
