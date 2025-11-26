using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerSettingData")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public float AccelerateTime { get; private set; } = 0.35f;
    [field: SerializeField] public float BaseSpeed { get; private set; } = 5f;
}
