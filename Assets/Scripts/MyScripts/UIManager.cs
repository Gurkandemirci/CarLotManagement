using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class UIManager : MonoBehaviour
{
    [SerializeField] private Image settingsImage;
    [SerializeField] private Image vibrationImage;
    [SerializeField] private Image soundsImage;
    [SerializeField] private Image musicImage;
    private bool isClicked = false;

    public void OpenSettings()
    {
        if (!isClicked)
        {
            settingsImage.DOFade(1, 0.5f);
            vibrationImage.DOFade(1, 0.5f);
            soundsImage.DOFade(1, 0.5f);
            musicImage.DOFade(1, 0.5f);
            isClicked = true;
        }
        else
        {
            settingsImage.DOFade(0, 0.2f);
            vibrationImage.DOFade(0, 0.2f);
            soundsImage.DOFade(0, 0.2f);
            musicImage.DOFade(0, 0.2f);
            isClicked = false;
        }
    }

}
