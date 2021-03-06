﻿using UnityEngine;
using UnityEditor;
using System.IO;

public static class ScriptableObjectUtility
{
    public static void CreateAsset<T>() where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();
        
        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets/ScriptableObjects/" + typeof(T).ToString() + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}