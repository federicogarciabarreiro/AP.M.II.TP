using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraManager))]
public class CameraManagerEditor : Editor
{

    CameraManager _target;

    private void OnEnable()
    {
        _target = (CameraManager)target;
        if (_target.positions == null) _target.positions = new List<Vector3>();
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        if (_target.playerCamera == null)
        {
            _target.playerCamera = (Camera)EditorGUILayout.ObjectField("Player Camera (F1)", _target.playerCamera, typeof(Camera), true);
        }
        EditorGUILayout.Space();
        _target.speed = EditorGUILayout.Slider(_target.speed, 0, 50);
        EditorGUILayout.Space();
        _target.lookPos = EditorGUILayout.Vector3Field("LookAtPoint", _target.lookPos);
        EditorGUILayout.Space();
        if (_target.positions.Count > 0)
        {
            for (int i = 0; i < _target.positions.Count; i++)
            {
                _target.positions[i] = EditorGUILayout.Vector3Field("Position [" + i + "]", _target.positions[i]);
                EditorGUILayout.Space();
            }
        }
        //................
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Position")) _target.AddValue();
        if (GUILayout.Button("Remove Last Position")) _target.RemoveValue();
        EditorGUILayout.EndHorizontal();
        //................
        if (GUILayout.Button("ResetAll")) _target.ResetAll();
    }
}
