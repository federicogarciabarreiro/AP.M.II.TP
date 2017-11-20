using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawGizmos : MonoBehaviour {

    #region var
    public Vector3 center, size, to, position;
    public List<Vector3> fromArray, toArray;
    public float fov, maxRange, minRange, aspect, radius;
    public Rect screenRect, _screenRect;
    public string _name;
    public Quaternion rotation;
    public Vector4 _rotationQuat;
    public Mesh mesh;
    public bool allowScaling, onSelect, drawCube, drawFrustum, drawLine, drawMesh, drawSphere, drawIcon;
    public List<bool> listOfBool = new List<bool>();
    #endregion

    private void Awake()
    {
        _screenRect = screenRect;
        fromArray = new List<Vector3>();
        toArray = new List<Vector3>();
    }

    private void OnDrawGizmos()
    {
        if (!onSelect) Draw();
    }

    private void OnDrawGizmosSelected()
    {
        if (onSelect) Draw();
    }

    private void Draw()
    {
        if (drawCube) Gizmos.DrawCube(center, size);
        if (drawFrustum) Gizmos.DrawFrustum(center, fov, maxRange, minRange, aspect);
        if (drawIcon) Gizmos.DrawIcon(center, _name, allowScaling);
        if (drawLine) DrawLine();
        if (drawMesh) Gizmos.DrawMesh(mesh, position, ConvertToQuaternion(_rotationQuat));
        if (drawSphere) Gizmos.DrawSphere(center, radius);
    }

    private void DrawLine()
    {
        for (int i = 0; i < fromArray.Count; i++)
        {
            Gizmos.DrawLine(fromArray[i], toArray[i]);
        }
    }

    Quaternion ConvertToQuaternion(Vector4 v4)
    {
        return new Quaternion(v4.x, v4.y, v4.z, v4.w);
    }
}
