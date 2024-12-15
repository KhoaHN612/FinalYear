using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathTriggerInteraction))]
public class PathLabelHandle : Editor
{
    private static GUIStyle labelStype;

    private void OnEnable()
    {
        labelStype = new GUIStyle
        {
            normal = new GUIStyleState
            {
                textColor = Color.white
            },
            alignment = TextAnchor.MiddleCenter,
            fontSize = 12,
            fontStyle = FontStyle.Bold
        };
    }

    private void OnSceneGUI()
    {
        PathTriggerInteraction pathTriggerInteraction = (PathTriggerInteraction)target;

        Handles.Label(pathTriggerInteraction.transform.position + Vector3.up * 2f, pathTriggerInteraction.currentPathType.ToString(), labelStype);
    }
}
