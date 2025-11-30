using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicHeightBlock : MonoBehaviour
{
    [SerializeField] private float minHeight = 0.1f;
    [SerializeField] private float maxHeight = 3f;
    [SerializeField] private float changeSpeed = 2f;
    [SerializeField] private FloatEventSO volumeChangeEvent;
    private float targetHeight = 1f;
    private float currentHeight = 1f;
    private Vector3 originScale;
    private Vector3 originPosition;
    void OnEnable()
    {
        volumeChangeEvent.OnEventRaised += OnVolumeChangeEvent;
    }
    void OnDisable()
    {
        volumeChangeEvent.OnEventRaised -= OnVolumeChangeEvent;
    }

    void Start()
    {
        originScale = transform.localScale;
        originPosition = transform.position;
        currentHeight = targetHeight;
        ApplyHeight();
    }

    void Update()
    {
        if (Mathf.Abs(currentHeight - targetHeight) >= 0.01f)
        {
            currentHeight = Mathf.Lerp(currentHeight, targetHeight, changeSpeed * Time.deltaTime);
            ApplyHeight();
        }
    }
    private void ApplyHeight()
    {
        Vector3 newScale = originScale;
        newScale.y = currentHeight;
        transform.localScale = newScale;

        Vector3 newPosition = originPosition;
        newPosition.y = originPosition.y + (currentHeight - originScale.y) * 0.5f;
        transform.position = newPosition;
    }

    private void OnVolumeChangeEvent(float value)
    {
        SetHeightNormalized(value);
    }
    private void SetHeightNormalized(float normalizedHeight)
    {
        targetHeight = Mathf.Lerp(minHeight, maxHeight, Mathf.Clamp01(normalizedHeight));
    }
}
