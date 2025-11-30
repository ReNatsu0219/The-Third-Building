using UnityEngine.UI;
using UnityEngine;

public class RoomMaskPanel : MonoBehaviour
{
    [SerializeField] private FloatEventSO brightnessChangeEvent;
    [SerializeField] private Image leftMask;
    [SerializeField] private Image rightMask;

    void OnEnable()
    {
        brightnessChangeEvent.OnEventRaised += OnbrightnessChangeEvent;
    }
    void OnDisable()
    {
        brightnessChangeEvent.OnEventRaised -= OnbrightnessChangeEvent;
    }

    private void OnbrightnessChangeEvent(float value)
    {
        leftMask.color = new Color(0f, 0f, 0f, 1f - CalculatePeakAtHalf(value));
        rightMask.color = new Color(0f, 0f, 0f, 1f - CalculatePeakAtOne(value));
    }
    private float CalculatePeakAtHalf(float x)
    {
        // 使用平滑脉冲函数，中心在0.5，宽度控制尖锐程度
        float pulseWidth = 0.3f; // 调整这个值改变峰值的宽度
        float centeredX = (x - 0.5f) / pulseWidth;
        return Mathf.Exp(-centeredX * centeredX);
    }

    // 在value≈1时接近1的函数  
    private float CalculatePeakAtOne(float x)
    {
        // 使用平滑阶跃函数的组合
        float steepness = 20f; // 调整这个值改变过渡的陡峭程度
        float activation = 1f / (1f + Mathf.Exp(-steepness * (x - 0.8f)));
        float deactivation = 1f / (1f + Mathf.Exp(-steepness * (0.7f - x)));
        return activation * deactivation;
    }
}
