using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AIInspector : EditorWindow
{
    [MenuItem("AI/Inspector")]
    static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(AIInspector));
    }

    public void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Actions", EditorStyles.boldLabel);


        if (GUILayout.Button("View Agent"))
        {
            Camera camera = Camera.main;
            GameObject go = Selection.activeGameObject;

            if (go.TryGetComponent(out AIAgent agent))
            {
                camera.transform.parent = agent.transform;
                camera.transform.localPosition = Vector3.back * 5 + Vector3.up * 3;
                camera.transform.localRotation = Quaternion.identity;
            }
        }
        GUILayout.EndHorizontal();
    }
}
