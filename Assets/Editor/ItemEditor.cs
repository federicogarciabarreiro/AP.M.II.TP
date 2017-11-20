using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class ItemEditor : EditorWindow
{
    private Object _focusObject;
    private string _searchAssetByName;
    private string _newName;
    private bool _rename, _delete;
    private List<Object> _assets = new List<Object>();

    [MenuItem("Manager/Script Manager")]
    static void Init()
    {
        ((ItemEditor)GetWindow(typeof(ItemEditor))).Show();
    }

    void OnGUI()
    {
        if (_focusObject == null) PrefabSearcher();
        else DoPrefabStuff();
    }

    private void DoPrefabStuff()
    {

        EditorGUILayout.LabelField("Options");
        if (GUILayout.Button("Clear")) _focusObject = null;
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical();
        if (_focusObject != null)
        {
            EditorGUILayout.LabelField(_focusObject.ToString());
        }
        GUI.color = Color.blue;
        EditorGUILayout.LabelField("Path: " + AssetDatabase.GetAssetPath(_focusObject), EditorStyles.whiteLabel);
        GUI.color = Color.white;
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Open", GUILayout.Width(50), GUILayout.Height(32))) AssetDatabase.OpenAsset(_focusObject);
        EditorGUILayout.EndHorizontal();
     
        EditorGUILayout.Space();
        if (GUILayout.Button("Renombrar")) _rename = !_rename;
        if (_rename)
        {
            EditorGUILayout.HelpBox("Are you sure ? This decision could influence the correct operation of the project.", MessageType.Warning);
            EditorGUILayout.BeginHorizontal();
            _newName = null;
            _newName = EditorGUILayout.TextField("New name", _newName);
            GUI.color = Color.green;
            if (GUILayout.Button("Accept")) AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(_focusObject), _newName);
            GUI.color = Color.white;
            EditorGUILayout.EndHorizontal();

        }
        EditorGUILayout.Space();
        if (GUILayout.Button("Delete")) _delete = !_delete;

        if (_delete)
        {
            EditorGUILayout.HelpBox("Are you sure ? This decision could influence the correct operation of the project.", MessageType.Warning);
            EditorGUILayout.BeginHorizontal();
            GUI.color = Color.red;
            if (GUILayout.Button("No")) _delete = false;
            GUI.color = Color.white;
            GUI.color = Color.green;
            if (GUILayout.Button("Yes")) AssetDatabase.MoveAssetToTrash(AssetDatabase.GetAssetPath(_focusObject));
            GUI.color = Color.white;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }
        
    }

    private void PrefabSearcher()
    {
        var aux = _searchAssetByName;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Search bar");
        _searchAssetByName = EditorGUILayout.TextField(aux);
        EditorGUILayout.EndHorizontal();

        int i;

        if (aux != _searchAssetByName)
        {
            _assets.Clear();

            string[] allPaths = AssetDatabase.FindAssets(_searchAssetByName);

            for (i = allPaths.Length - 1; i >= 0; i--) _assets.Add(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(allPaths[i]), typeof(Object))); //AssetDatabase.LoadAssetAtPath carga un objeto en una ubicacion
        }

        for (i = _assets.Count - 1; i >= 0; i--)
        {
            //.................
            EditorGUILayout.BeginHorizontal();
            //.................
            EditorGUILayout.BeginVertical();
            if (_assets[i] != null) EditorGUILayout.LabelField(_assets[i].ToString());
            GUI.color = Color.blue;
            EditorGUILayout.LabelField("Path: " + AssetDatabase.GetAssetPath(_assets[i]), EditorStyles.whiteLabel);
            GUI.color = Color.white;
            EditorGUILayout.EndVertical();
            //.................
            if (GUILayout.Button("Seleccionar", GUILayout.Width(100), GUILayout.Height(32))) _focusObject = _assets[i];
            EditorGUILayout.EndHorizontal();
            //.................
        }
    }
}