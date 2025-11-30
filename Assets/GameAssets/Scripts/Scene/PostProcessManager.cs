using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessManager : MonoSingleton<PostProcessManager>
{
    [field: Header("Event System")]
    [SerializeField] private FloatEventSO brightnessChangeEvent;
    [field: Header("Post-Processing")]
    [SerializeField] private Volume globalVolume;
    [SerializeField] private Volume darkRoom1Volume;
    [SerializeField] private Volume darkRoom2Volume;
    private ColorAdjustments colorAdjustments;

    private float brightnessValue;
    protected override void Awake()
    {
        base.Awake();

        if (globalVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            if (!colorAdjustments.postExposure.overrideState)
            {
                colorAdjustments.postExposure.overrideState = true;
            }
        }
    }
    void OnEnable()
    {
        brightnessChangeEvent.OnEventRaised += OnbrightnessChangeEvent;
    }
    void OnDisable()
    {
        brightnessChangeEvent.OnEventRaised -= OnbrightnessChangeEvent;
    }

    #region Event Methods
    private void OnbrightnessChangeEvent(float value)
    {
        brightnessValue = value;

        colorAdjustments.postExposure.value = value;
    }
    #endregion
}
