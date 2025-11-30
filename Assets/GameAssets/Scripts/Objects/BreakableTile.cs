using System;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 可被吃掉并消失、加载场景时会重新出现的可脚本化瓦片
/// </summary>
[Serializable]
public class BreakableTile : TileBase
{
    [SerializeField] private Sprite m_Sprite;

    // === 核心：Tile 被吃掉后需要临时“隐藏”，但不保存这个状态 ===
    // 我们只在运行时记，而不写入任何存档。
    private static HashSet<Vector3Int> eatenTiles = new HashSet<Vector3Int>();

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        tilemap.RefreshTile(position);
    }

    public override void GetTileData(
        Vector3Int position,
        ITilemap tilemap,
        ref TileData tileData
    )
    {
        tileData.transform = Matrix4x4.identity;
        tileData.color = Color.white;

        // 如果此 tile 曾经被吃 —— 不渲染
        if (eatenTiles.Contains(position))
        {
            tileData.sprite = null;
            tileData.colliderType = Tile.ColliderType.None;
        }
        else
        {
            tileData.sprite = m_Sprite;
            tileData.colliderType = Tile.ColliderType.Sprite;
        }
    }

    /// <summary>
    /// 供外部（如玩家“吃”系统）调用：
    /// Tile 被吃掉
    /// </summary>
    public void EatTile(Vector3Int position)
    {
        if (!eatenTiles.Contains(position))
            eatenTiles.Add(position);
    }

    /// <summary>
    /// LOAD 时调用，所有 breakable tile 重新出现
    /// </summary>
    public static void ResetAllTiles()
    {
        eatenTiles.Clear();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(BreakableTile))]
public class BreakableTileEditor : Editor
{
    private BreakableTile tile => target as BreakableTile;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Breakable Tile: 被吃掉后会消失，加载场景时恢复");
        EditorGUILayout.Space();

        tile.GetType()
            .GetField("m_Sprite", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(tile, (Sprite)EditorGUILayout.ObjectField("Sprite",
                      (Sprite)tile.GetType()
                          .GetField("m_Sprite",
                             System.Reflection.BindingFlags.NonPublic |
                             System.Reflection.BindingFlags.Instance)
                          .GetValue(tile),
                      typeof(Sprite),
                      false));

        if (GUI.changed)
            EditorUtility.SetDirty(tile);
    }

    [MenuItem("Assets/Create/BreakableTile")]
    public static void CreateBreakableTile()
    {
        string path = EditorUtility.SaveFilePanelInProject(
            "Create Breakable Tile",
            "New Breakable Tile",
            "asset",
            "Create new Breakable Tile"
        );
        if (path == "")
            return;

        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<BreakableTile>(), path);
    }
}
#endif
