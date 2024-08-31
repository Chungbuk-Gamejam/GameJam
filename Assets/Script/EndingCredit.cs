using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCredit : MonoBehaviour
{
    [SerializeField] Animator _pixelEndingCredit;
    [SerializeField] Animator _textEndingCredit;

    public void OnEnable()
    {
        if (_pixelEndingCredit != null)
        {
            _pixelEndingCredit.gameObject.SetActive(true);
            _pixelEndingCredit.Play($"EndingCredit_Credit1");
        }
    }

    public void PixelEndingCreditComplete()
    {
        if (_pixelEndingCredit != null)
        {
            _pixelEndingCredit.gameObject.SetActive(false);
        }

        if (_textEndingCredit != null)
        {
            _textEndingCredit.gameObject.SetActive(true);
            _textEndingCredit.Play($"EndingCredit_Credit2");
        }
    }
}
