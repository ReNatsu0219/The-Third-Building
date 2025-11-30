using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [field: Header("Canvas")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Image deathPanel;
    [SerializeField] private Image deathMask;

    [SerializeField] private float expandDuration;
    [SerializeField] private float holdDuration;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float endScaleBase;

    [field: Header("Special")]
    [SerializeField] private string timeStopRoomName;
    private RectTransform deathMaskRect;
    private CanvasGroup deathCanvasGroup;
    public Canvas Canva;
    public Image blackScreen;

    [field: Header("Event System")]
    [SerializeField] private VoidEventSO loadGameEvent;
    [SerializeField] private VoidEventSO pauseEvent;
    [SerializeField] private VoidEventSO playerDeathEvent;

    protected override void Awake()
    {
        base.Awake();
        InitBlackScreenImage();

        deathMaskRect = deathMask.gameObject.GetComponent<RectTransform>();
        deathCanvasGroup = deathPanel.gameObject.GetComponent<CanvasGroup>();
    }
    void OnEnable()
    {
        pauseEvent.OnEventRaised += TogglePausePanel;
        playerDeathEvent.OnEventRaised += OnPlayerDeathEvent;
    }

    void OnDisable()
    {
        pauseEvent.OnEventRaised -= TogglePausePanel;
        playerDeathEvent.OnEventRaised += OnPlayerDeathEvent;
    }



    private void Start()
    {
        StartCoroutine(BlackScreenFade(0f, 2f));
    }

    public Image GetBlackScreen()
    {
        if (blackScreen == null)
        {
            Debug.LogWarning("BlackScreen Image no Setting��");
        }
        return blackScreen;
    }

    private void InitBlackScreenImage()
    {
        blackScreen.GetComponent<CanvasGroup>().alpha = 1f;
    }

    public IEnumerator BlackScreenFade(float endValue, float time)
    {
        blackScreen.DOFade(endValue, time);
        yield break;
    }

    #region Event Methods
    private void TogglePausePanel()
    {
        //if is not in timeStopRoom(level 12),stop time normally
        if (RoomManager.Instance.CurrentRoom.roomName != timeStopRoomName)
        {
            if (pausePanel.activeInHierarchy)
            {
                Time.timeScale = 1f;
                pausePanel.SetActive(false);
            }
            else
            {
                Time.timeScale = 0f;
                pausePanel.SetActive(true);
            }
        }
        //else it won't stop time but will open pausepanel
        else
        {
            if (pausePanel.activeInHierarchy)
            {
                pausePanel.SetActive(false);
            }
            else
            {
                pausePanel.SetActive(true);
            }
        }
    }
    private void OnPlayerDeathEvent()
    {
        StartCoroutine(DeathMaskCoroutine());
    }
    #region Death Mask
    private IEnumerator DeathMaskCoroutine()
    {
        InitializeDeathMask();

        yield return StartCoroutine(ExpandDeathMaskRoutine());

        loadGameEvent.RaiseEvent();

        yield return new WaitForSeconds(holdDuration);

        yield return StartCoroutine(DisappearDeathMaskRoutine());
    }

    private IEnumerator ExpandDeathMaskRoutine()
    {
        float timer = 0f;
        Vector3 startScale = Vector3.zero;
        float maxScale = CalculateMaxScale() * endScaleBase;
        Vector3 endScale = new Vector3(maxScale, maxScale, 1f);

        while (timer < expandDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / expandDuration;

            float easedProgress = EaseOutCubic(progress);
            deathMaskRect.localScale = Vector3.Lerp(startScale, endScale, easedProgress);

            yield return null;
        }

        deathMaskRect.localScale = endScale;
    }
    private IEnumerator DisappearDeathMaskRoutine()
    {
        float timer = 0f;

        while (timer < expandDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / expandDuration;

            float easedProgress = EaseOutCubic(progress);
            deathCanvasGroup.alpha = 1f - easedProgress;

            yield return null;
        }

        deathCanvasGroup.alpha = 0f;
        deathPanel.gameObject.SetActive(false);
    }



    private float CalculateMaxScale()
    {
        float screenRatio = (float)Screen.width / Screen.height;
        return screenRatio > 1 ? screenRatio * 1.5f : 1.5f;
    }
    private float EaseOutCubic(float x) => 1f - Mathf.Pow(1f - x, 3f);

    private void InitializeDeathMask()
    {
        deathCanvasGroup.alpha = 1f;
        deathPanel.gameObject.SetActive(true);

        deathMaskRect.localScale = Vector3.zero;

        Vector2 screenPos = Camera.main.WorldToScreenPoint(PlayerManager.Instance.player.transform.position);
        deathMaskRect.position = screenPos;
    }
    #endregion
    #endregion
}
