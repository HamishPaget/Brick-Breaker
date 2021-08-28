using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(PlayerMovement))]
[CanEditMultipleObjects]
public class PlayerMovementEditor : Editor
{
    SerializedProperty useMovementCurve;

    void OnEnable()
    {
        useMovementCurve = serializedObject.FindProperty("useMovementCurve");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();


        useMovementCurve.boolValue = EditorGUILayout.Toggle("Use Movement Curve", useMovementCurve.boolValue);
        //EditorGUILayout.PropertyField(useMovementCurve);

        
        EditorGUI.indentLevel++;
        if (useMovementCurve.boolValue)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("moveSpeedOverTime"));
        }
        else
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("moveSpeed"));
        }
        EditorGUI.indentLevel--;



        DrawPropertiesExcluding(serializedObject, "m_Script", "moveSpeedOverTime", "moveSpeed", "useMovementCurve");

        serializedObject.ApplyModifiedProperties();

    }
}