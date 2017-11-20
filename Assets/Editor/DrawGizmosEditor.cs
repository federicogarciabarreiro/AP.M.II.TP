using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DrawGizmos))]
public class DrawGizmosEditor : Editor {

    DrawGizmos _target;
    bool _bool;

    void OnEnable()
    {
        _target = (DrawGizmos)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical(GUILayout.Width(250));

        #region ButtonOnTop
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        if (!_target.onSelect && GUILayout.Button("OnDrawGizmos", GUILayout.Width(150), GUILayout.Height(18))) _target.onSelect = true;
        if (_target.onSelect && GUILayout.Button("OnDrawGizmosSelected", GUILayout.Width(150), GUILayout.Height(18))) _target.onSelect = false;
        if (GUILayout.Button("Reset All", GUILayout.Width(100), GUILayout.Height(18)))
        {
            _target.center = Vector3.zero;
            _target.size = Vector3.zero;
            _target.aspect = 0;
            _target.fov = 0;
            _target.minRange = 0;
            _target.maxRange = 0;
            _target.screenRect = _target._screenRect;
            _target._name = null;
            _target.allowScaling = false;
            _target.fromArray.Clear();
            _target.toArray.Clear();
            _target.mesh = null;
            _target.position = Vector3.zero;
            _target._rotationQuat = Vector4.zero;
            _target.radius = 0;

                _target.drawFrustum = false;
                _target.drawIcon = false;
                _target.drawLine = false;
                _target.drawMesh = false;
                _target.drawSphere = false;
                _target.drawCube = false;
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        #endregion

        #region DrawCube
        //.............................
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("DrawCube", GUILayout.Width(200), GUILayout.Height(18)))
        {
            _target.drawCube = !_target.drawCube;

            if (_target.drawCube == true)
            {
                _target.drawFrustum = false;
                _target.drawIcon = false;
                _target.drawLine = false;
                _target.drawMesh = false;
                _target.drawSphere = false;
            }
        }
        if (_target.drawCube && GUILayout.Button("Reset", GUILayout.Width(50), GUILayout.Height(18)))
        {
            
            _target.center = Vector3.zero;
            _target.size = Vector3.zero;
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        if (_target.drawCube)
        {
            _target.center = EditorGUILayout.Vector3Field("Center:", _target.center);
            EditorGUILayout.Space();
            _target.size = EditorGUILayout.Vector3Field("Size:", _target.size);

            Repaint();
        }
        EditorGUILayout.Space();
        //.............................
        #endregion

        #region DrawFrustum
        //.............................
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("DrawFrustum", GUILayout.Width(200), GUILayout.Height(18)))
        {
            _target.drawFrustum = !_target.drawFrustum;

            if (_target.drawFrustum == true)
            {
                _target.drawIcon = false;
                _target.drawLine = false;
                _target.drawMesh = false;
                _target.drawSphere = false;
                _target.drawCube = false;
            }
        }
        if (_target.drawFrustum && GUILayout.Button("Reset", GUILayout.Width(50), GUILayout.Height(18)))
        {
            _target.center = Vector3.zero;
            _target.aspect = 0;
            _target.fov = 0;
            _target.minRange = 0;
            _target.maxRange = 0;
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        if (_target.drawFrustum)
        {
            _target.center = EditorGUILayout.Vector3Field("Center:", _target.center);
            EditorGUILayout.Space();
            _target.aspect = EditorGUILayout.FloatField("Width:", _target.aspect);
            EditorGUILayout.Space();
            _target.fov = EditorGUILayout.FloatField("High:", _target.fov);
            EditorGUILayout.Space();
            _target.minRange = EditorGUILayout.FloatField("MinRange:", _target.minRange);
            EditorGUILayout.Space();
            _target.maxRange = EditorGUILayout.FloatField("MaxRange:", _target.maxRange);

            Repaint();
        }
        EditorGUILayout.Space();
        //.............................
        #endregion

        #region DrawLine
        //.............................
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("DrawLine", GUILayout.Width(200), GUILayout.Height(18)))
        {
            _target.drawLine = !_target.drawLine;

            if (_target.drawLine == true)
            {
                _target.drawIcon = false;
                _target.drawFrustum = false;
                _target.drawMesh = false;
                _target.drawSphere = false;
                _target.drawCube = false;
            }
        }
        if (_target.drawLine && GUILayout.Button("Reset", GUILayout.Width(50), GUILayout.Height(18)))
        {
            _target.fromArray.Clear();
            _target.toArray.Clear();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        if (_target.drawLine)
        {
            for (int i = 0; i < _target.fromArray.Count; i++)
            {
                _target.fromArray[i] = EditorGUILayout.Vector3Field("From (" + i + "):", _target.fromArray[i]);
                _target.toArray[i] = EditorGUILayout.Vector3Field("To (" + i + "):", _target.toArray[i]);
                EditorGUILayout.Space();
                Repaint();
            }

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add", GUILayout.Width(100), GUILayout.Height(18)))
            {
                _target.fromArray.Add(new Vector3(0, 0, 0));
                _target.toArray.Add(new Vector3(0, 0, 0));
            }
            if (GUILayout.Button("Remove", GUILayout.Width(100), GUILayout.Height(18)))
            {
                _target.fromArray.RemoveAt(_target.fromArray.Count - 1);
                _target.toArray.RemoveAt(_target.toArray.Count - 1);
            }
            GUILayout.EndHorizontal();

            EditorGUILayout.Space();

            Repaint();
        }
        EditorGUILayout.Space();
        //.............................
        #endregion

        #region DrawMesh
        //.............................
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("DrawMesh", GUILayout.Width(200), GUILayout.Height(18)))
        {
            _target.drawMesh = !_target.drawMesh;

            if (_target.drawMesh == true)
            {
                _target.drawIcon = false;
                _target.drawFrustum = false;
                _target.drawLine = false;
                _target.drawSphere = false;
                _target.drawCube = false;
            }
        }
        if (_target.drawMesh && GUILayout.Button("Reset", GUILayout.Width(50), GUILayout.Height(18)))
        {
            _target.mesh = null;
            _target.position = Vector3.zero;
            _target._rotationQuat = Vector4.zero;
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        if (_target.drawMesh)
        {

            _target.mesh = (Mesh)EditorGUILayout.ObjectField("Mesh", _target.mesh, typeof(Mesh), false);
            _target.position = EditorGUILayout.Vector3Field("Position:", _target.position);
            _target._rotationQuat = EditorGUILayout.Vector4Field("Rotation:", _target._rotationQuat);

            EditorGUILayout.Space();

            Repaint();
        }
        EditorGUILayout.Space();
        //.............................
        #endregion

        #region DrawSphere
        //.............................
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("DrawSphere", GUILayout.Width(200), GUILayout.Height(18)))
        {
            _target.drawSphere = !_target.drawSphere;


            if (_target.drawSphere == true)
            {
                _target.drawIcon = false;
                _target.drawFrustum = false;
                _target.drawLine = false;
                _target.drawMesh = false;
                _target.drawCube = false;
            }
        }

        if (_target.drawSphere && GUILayout.Button("Reset", GUILayout.Width(50), GUILayout.Height(18)))
        {
            _target.center = Vector3.zero;
            _target.radius = 0;
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        if (_target.drawSphere)
        {
            _target.center = EditorGUILayout.Vector3Field("Center:", _target.center);
            EditorGUILayout.Space();
            _target.radius = EditorGUILayout.FloatField("Radius:", _target.radius);

            EditorGUILayout.Space();

            Repaint();
        }
        EditorGUILayout.Space();
        //.............................
        #endregion

        EditorGUILayout.EndVertical();
    }
}
