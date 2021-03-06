﻿
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelSettings))]

public class LevelSettings_Editor : Editor
{
    private void OnEnable()
    {
        LevelSettings listScript;
        listScript = (LevelSettings)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        base.OnInspectorGUI();

        var listScript = target as LevelSettings;

        if (GUILayout.Button("Fetch Enemies"))
        {
            listScript.FetchEnemies();

            EditorUtility.SetDirty(target);

            this.serializedObject.ApplyModifiedProperties();
        }

        if (GUILayout.Button("Save position on player positions"))
        {
            listScript.AddPositionPointerToSpawn();

            EditorUtility.SetDirty(target);

            this.serializedObject.ApplyModifiedProperties();
        }
        if (GUILayout.Button("Save position on enemy positions"))
        {
            listScript.AddPositionPointerToEnemyPositions();

            EditorUtility.SetDirty(target);

            this.serializedObject.ApplyModifiedProperties();
        }

        
    }

}
