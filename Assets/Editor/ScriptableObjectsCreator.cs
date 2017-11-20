using UnityEngine;
using UnityEditor;

public class ScriptableObjectCreator
{
    [MenuItem("Manager/Create PlayerSettings (ScriptableObject)")]
    public static void CreateScriptableObjectConfig()
    {
        ScriptableObjectUtility.CreateAsset<ScriptableObjectConfig>();
    }

    [MenuItem("Manager/Create PrefabsGridArray (ScriptableObject)")]
    public static void CreateScriptableObjectConfig0()
    {
        ScriptableObjectUtility.CreateAsset<PrefabsGridArray>();
    }
}