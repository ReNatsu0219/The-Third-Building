using UnityEngine;
using UnityEngine.AddressableAssets;

public enum SceneType
{
    Menu, Location
}

[CreateAssetMenu(menuName = "Scene/GameSceneSO")]
public class GameSceneSO : ScriptableObject
{
    public AssetReference sceneReference;
    public SceneType sceneType;
}
