using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(ScriptableObjectConfig))]
public class ScriptableObjectConfigEditor : Editor {

    private ScriptableObjectConfig _target;

	void OnEnable()
    {
        _target = (ScriptableObjectConfig)target;
    }

    public override void OnInspectorGUI()
    {
        //...................
        _target.characterName = EditorGUILayout.TextField("Character name", _target.characterName);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Level", currentLevel().ToString());
        EditorGUILayout.Space();
        _target.xp = EditorGUILayout.IntField("XP required", _target.xp);
        EditorGUILayout.Space();
        _target.currentXp = EditorGUILayout.IntField("Current XP", _target.currentXp);
        EditorGUILayout.Space();
        //...................
        Rect r = EditorGUILayout.BeginVertical();
        var aux = auxValue();
        if (aux < 0) aux = -aux;
        var progressBarAux = (aux / _target.xp) * 100;
        EditorGUI.ProgressBar(r, progressBarAux / 100, (aux).ToString() + " / " + _target.xp.ToString());
        GUILayout.Space(18);
        EditorGUILayout.EndVertical();
        //...................
        EditorGUILayout.Space();
        _target.hp = EditorGUILayout.IntField("HP", _target.hp);
        EditorGUILayout.Space();
        _target.armor = EditorGUILayout.IntField("Armor", _target.armor);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Attributes");
        EditorGUILayout.Space();
        //...................
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        _target.agility = EditorGUILayout.IntSlider("Agility", _target.agility, 0, 10);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        _target.strength = EditorGUILayout.IntSlider("Strength", _target.strength, 0, 10);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        _target.intellect = EditorGUILayout.IntSlider("Intellect", _target.intellect, 0, 10);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        _target.stamina = EditorGUILayout.IntSlider("Stamina", _target.stamina, 0, 10);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        _target.spirit = EditorGUILayout.IntSlider("Spirit", _target.spirit, 0, 10);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();
        //...................
        EditorGUILayout.Space();
        _target.speed = EditorGUILayout.IntField("Speed", _target.speed);
        EditorGUILayout.Space();
        _target.jumpSpeed = EditorGUILayout.IntField("Jump Speed", _target.jumpSpeed);
        FixValues();   
    }

    float auxValue()
    {
        if (_target.currentXp <= 0 || _target.xp <= 0) FixValues();
        var auxV = (((_target.currentXp - ((_target.level + 1) * _target.xp))) + _target.xp);   
        return auxV;
    }

    int currentLevel()
    {
        var currentLevel = (int)(_target.currentXp / _target.xp);
        if (currentLevel <= 0)
        {
            return 0;
        }
        else {
            _target.level = currentLevel;
            return _target.level;
        }
    }

    void FixValues()
    {
        Repaint();

        float aux = _target.agility + _target.strength + _target.intellect + _target.spirit + _target.stamina;
        if (_target.xp <= 0) _target.xp = 1;
        if (_target.currentXp <= 0) _target.currentXp = 1;
        if (_target.xp >= 1000000) _target.xp = 1000000;
        if (_target.hp < 0) _target.hp = 0;
        if (_target.hp > 100) _target.hp = 100;
        if (_target.armor < 0) _target.armor = 0;
        if (_target.armor > 150) _target.armor = 150;
        if (_target.speed <= 0) _target.speed = 1;
        if (_target.jumpSpeed <= 0) _target.jumpSpeed = 1;
        if (_target.speed > 100) _target.speed = 100;
        if (_target.jumpSpeed > 100) _target.jumpSpeed = 100;
        if (_target.level <= 0) _target.level = 1;
        if (aux > 10)
        {
            _target.agility--;
            _target.strength--;
            _target.intellect--;
            _target.stamina--;
            _target.spirit--;
        }
        if (aux < 10)
        {       
            var auxN = aux - 10;
            string auxS = "Attribute points available: " + -auxN;

            EditorGUILayout.HelpBox(auxS, MessageType.Info);
        }

    }
}