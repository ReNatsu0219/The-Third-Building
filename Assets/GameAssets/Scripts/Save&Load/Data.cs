using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public string sceneToSave;
    public Dictionary<string, SerializableVector3> characterPosDict = new Dictionary<string, SerializableVector3>();
    public Dictionary<string, float> floatSavedData = new Dictionary<string, float>();
    public void SaveGameScene(GameSceneSO savedScene)
    {
        sceneToSave = JsonUtility.ToJson(savedScene);
    }
    public GameSceneSO GetSavedScene()
    {
        var newScene = ScriptableObject.CreateInstance<GameSceneSO>();
        JsonUtility.FromJsonOverwrite(sceneToSave, newScene);
        return newScene;
    }
}
public class SerializableVector3
{
    public float x, y, z;
    public SerializableVector3(Vector3 v3)
    {
        this.x = v3.x;
        this.y = v3.y;
        this.z = v3.z;
    }
    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}
