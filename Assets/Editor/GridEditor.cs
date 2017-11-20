﻿using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Grid))]
public class GridEditor : Editor
{

    #region var
    Grid _target;

    private int oldIndex = 0;
    private int oldIndexB = 0;

    private bool showSettings = false;
    private bool remove = true;
    #endregion

    void OnEnable()
    {
        _target = (Grid)target;
    }

    public override void OnInspectorGUI()
    {
        //.............
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        //.............
        EditorGUILayout.LabelField("Grid Settings", GUILayout.Width(100));
        showSettings = EditorGUILayout.Toggle("", showSettings);
        //.............
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        //.............
        if (showSettings)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical();
            _target.color = EditorGUILayout.ColorField("Grid Color", _target.color);
            EditorGUILayout.Space();
            _target.width = EditorGUILayout.Slider("Width", _target.width, 0, 100);
            EditorGUILayout.Space();
            _target.height = EditorGUILayout.Slider("Height", _target.height, 0, 100);
            EditorGUILayout.Space();
            EditorGUI.BeginChangeCheck();
            var newPrefabsGridArray = (PrefabsGridArray)EditorGUILayout.ObjectField("Prefabs Grid Array", _target.prefabsGridArray, typeof(PrefabsGridArray), false);
            if (EditorGUI.EndChangeCheck()) _target.prefabsGridArray = newPrefabsGridArray;
            EditorGUILayout.Space();
            if (remove) remove = EditorGUILayout.Toggle("2-Click (On - Remove)", remove);
            if (!remove) remove = EditorGUILayout.Toggle("2-Click (Off - Add)", remove);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            //.................
            if (!_target.currentPrefab && _target.prefabsGridArray) _target.currentPrefab = _target.prefabsGridArray.prefabs[0];
            if (_target.width != _target.height) EditorGUILayout.HelpBox("The proportion does not have a ratio of 1x1", MessageType.Info);
        }
        //.............
        if (_target.prefabsGridArray != null)
        {
            EditorGUI.BeginChangeCheck();
            var names = new string[_target.prefabsGridArray.prefabs.Length];
            var values = new int[names.Length];

            for (int i = 0; i < names.Length; i++)
            {
                names[i] = _target.prefabsGridArray.prefabs[i] != null ? _target.prefabsGridArray.prefabs[i].name : "";
                values[i] = i;
            }

            var index = EditorGUILayout.IntPopup("Prefab", oldIndex, names, values);

            if (EditorGUI.EndChangeCheck())
            {
                if (oldIndex != index)
                {
                    oldIndex = index;
                    _target.currentPrefab = _target.prefabsGridArray.prefabs[index];

                    float width = _target.currentPrefab.GetComponent<Renderer>().bounds.size.x;
                    float height = _target.currentPrefab.GetComponent<Renderer>().bounds.size.y;

                    _target.width = width;
                    _target.height = height;

                }
            }


        }
        //.............
        if (!remove)
        {
            if (_target.prefabsGridArray != null)
            {
                EditorGUI.BeginChangeCheck();
                var namesB = new string[_target.prefabsGridArray.prefabs.Length];
                var valuesB = new int[namesB.Length];

                for (int i = 0; i < namesB.Length; i++)
                {
                    namesB[i] = _target.prefabsGridArray.prefabs[i] != null ? _target.prefabsGridArray.prefabs[i].name : "";
                    valuesB[i] = i;
                }

                var indexB = EditorGUILayout.IntPopup("PrefabB", oldIndexB, namesB, valuesB);

                if (EditorGUI.EndChangeCheck())
                {
                    if (oldIndexB != indexB)
                    {
                        oldIndexB = indexB;
                        _target.currentPrefabB = _target.prefabsGridArray.prefabs[indexB];

                        float width = _target.currentPrefabB.GetComponent<Renderer>().bounds.size.x;
                        float height = _target.currentPrefabB.GetComponent<Renderer>().bounds.size.y;

                        _target.width = width;
                        _target.height = height;

                    }
                }
            }
        }
        //.............
        EditorGUILayout.Space();
        //.............      
        if (GUILayout.Button("Remove All"))
        {
            foreach (var item in _target.objects)
            {
                DestroyImmediate(item);
                Repaint();
            }
            _target.objects.Clear();
        }
    }

    void OnSceneGUI()
    {

        int controlId = GUIUtility.GetControlID(FocusType.Passive);
        Ray ray = Camera.current.ScreenPointToRay(new Vector3(Event.current.mousePosition.x, -Event.current.mousePosition.y + Camera.current.pixelHeight));

        if (Event.current.isMouse)
        {
            if (Event.current.type == EventType.MouseDown)
            {
                if (Event.current.button == 0)
                {
                    GUIUtility.hotControl = controlId;
                    Event.current.Use();

                    if (_target.currentPrefab)
                    {
                        Vector3 aligned = new Vector3(Mathf.Floor(ray.origin.x / _target.width) * _target.width + _target.width / 2.0f, Mathf.Floor(ray.origin.y / _target.height) * _target.height + _target.height / 2.0f, 0.0f);

                        if (GetAlign(aligned) != null) return;

                        var block = (GameObject)PrefabUtility.InstantiatePrefab(_target.currentPrefab.gameObject);
                        block.transform.position = aligned;
                        block.transform.parent = _target.transform;
                        _target.objects.Add(block);
                    }
                }

                if (Event.current.button == 1)
                {
                    if (remove)
                    {
                        GUIUtility.hotControl = controlId;
                        Event.current.Use();

                        Vector3 aligned = new Vector3(Mathf.Floor(ray.origin.x / _target.width) * _target.width + _target.width / 2.0f, Mathf.Floor(ray.origin.y / _target.height) * _target.height + _target.height / 2.0f, 0.0f);
                        Transform transform = GetAlign(aligned);

                        if (transform != null) DestroyImmediate(transform.gameObject);
                    }
                    if (!remove)
                    {
                        GUIUtility.hotControl = controlId;
                        Event.current.Use();

                        if (_target.currentPrefabB)
                        {
                            Vector3 aligned = new Vector3(Mathf.Floor(ray.origin.x / _target.width) * _target.width + _target.width / 2.0f, Mathf.Floor(ray.origin.y / _target.height) * _target.height + _target.height / 2.0f, 0.0f);

                            if (GetAlign(aligned) != null) return;

                            var block = (GameObject)PrefabUtility.InstantiatePrefab(_target.currentPrefabB.gameObject);
                            block.transform.position = aligned;
                            block.transform.parent = _target.transform;
                            _target.objects.Add(block);
                        }
                    }
                }

                if ((Event.current.type == EventType.MouseUp)) GUIUtility.hotControl = 0;
            }
        }
    }

    Transform GetAlign(Vector3 aligned)
    {
        int i = 0;
        while (i < _target.transform.childCount)
        {
            Transform transform = _target.transform.GetChild(i);
            if (transform.position == aligned)
            {
                return transform;
            }
            i++;
        }
        return null;
    }
}