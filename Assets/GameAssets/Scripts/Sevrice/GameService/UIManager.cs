using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    public Canvas Canva;
    public Image blackScreen;

    protected override void Awake()
    {
        base.Awake();
        InitBlackScreenImage();
    }

    private void Start()
    {
        StartCoroutine(BlackScreenFade(0f, 2f));
    }

    public Image GetBlackScreen()
    {
        if (blackScreen == null)
        {
            Debug.LogWarning("BlackScreen Image no Setting£¡");
        }
        return blackScreen;
    }

    private void InitBlackScreenImage()
    {
        blackScreen.GetComponent<CanvasGroup>().alpha = 1f;
    }

    public IEnumerator BlackScreenFade(float endValue,float time)
    {
        blackScreen.DOFade(endValue, time);
        yield break;
    }
}
