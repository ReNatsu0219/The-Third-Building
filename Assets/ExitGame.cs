using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour, IEatable
{
    [SerializeField] private Sprite beforeSprite;
    [SerializeField] private Sprite afterSprite;
    [SerializeField] private SpriteRenderer r;
    private float quitTimer = 0f;
    private bool isQuitting = false;
    void Awake()
    {
        r.sprite = beforeSprite;
    }
    public bool BeEaten()
    {
        r.sprite = afterSprite;
        QuitGame();
        return false;
    }
    public void QuitGame()
    {
        if (isQuitting) return;
        isQuitting = true;

        Debug.Log("正在退出游戏...");

        // 延迟执行实际退出，给效果时间播放
        Invoke("ExecuteQuit", 0.5f);
    }
    void ExecuteQuit()
    {
#if UNITY_EDITOR
        // 编辑器模式下
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("在Unity编辑器中停止运行");
#else
        // 打包后运行时
        Application.Quit();
#endif
    }
}
