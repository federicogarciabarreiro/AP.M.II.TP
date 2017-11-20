using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor
{

    LevelGenerator _target;

    private void OnEnable()
    {
        _target = (LevelGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        if (!_target.wallMaterial) _target.wallMaterial = (Material)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/LevelGenerator/Block (1).mat", typeof(Material));
        if (!_target.baseMaterial) _target.baseMaterial = (Material)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/LevelGenerator/Empty (0).mat", typeof(Material));
        if (!_target.slot) _target.slot = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/LevelGenerator/Slot.prefab", typeof(GameObject));
        EditorGUILayout.Space();
        //..............
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Level Generator", GUILayout.Width(150), GUILayout.Height(18));
        if (GUILayout.Button("Randomize"))
        {
            _target.width = Random.Range(1, 15);
            _target.height = Random.Range(1, 15);
        }
        if (GUILayout.Button("Reset"))
        {
            _target.width = 0;
            _target.height = 0;
        }
        if (_target.width > 15) _target.width = 15;
        if (_target.height > 15) _target.height = 15;
        if (_target.width < 0) _target.width = 0;
        if (_target.height < 0) _target.height = 0;
        EditorGUILayout.EndHorizontal();
        //..............
        EditorGUILayout.Space();
        _target.width = (int)EditorGUILayout.Slider("Width", _target.width, 0f, 15f);
        EditorGUILayout.Space();
        _target.height = (int)EditorGUILayout.Slider("Height", _target.height, 0f, 15f);
        EditorGUILayout.Space();
        _target.baseMaterial.color = EditorGUILayout.ColorField("BaseMaterial",_target.baseMaterial.color);
        EditorGUILayout.Space();
        _target.wallMaterial.color = EditorGUILayout.ColorField("WallMaterial", _target.wallMaterial.color);
        EditorGUILayout.Space();
        //..............
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Generate", GUILayout.Width(200), GUILayout.Height(30)) && _target.width > 0 && _target.height > 0)
        {
            if (_target.level == null) _target.Init();
            else _target.InitAndDestroy();
        }
        if (GUILayout.Button("Destroy", GUILayout.Width(75), GUILayout.Height(30))) _target.LevelDestroy();
        EditorGUILayout.EndHorizontal();
        //..............
        if (_target.width == 0 || _target.height == 0) EditorGUILayout.HelpBox("If the indicated width or height is equal to 0, the level can not be generated", MessageType.Info);
        if (_target.width == 15 || _target.height == 15) EditorGUILayout.HelpBox("The maximum scale is 15x15 due to perfomance reasons", MessageType.Info);
        EditorGUILayout.Space();
    }
}
