using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[ExecuteInEditMode]
public class Grid : MonoBehaviour
{

    #region var
    public float width = 1f;
    public float height = 1f;
    float offSet = 50f;

    public Color color = Color.green;
    public Transform currentPrefab, currentPrefabB;
    public PrefabsGridArray prefabsGridArray;
    public List<GameObject> objects = new List<GameObject>();
    List<GameObject> _objects = new List<GameObject>();
    #endregion

    private void Awake()
    {
        if (objects.Count <= 0)
        { 
            Transform[] allChildren = GetComponentsInChildren<Transform>();
            foreach (var item in allChildren)
            {
                _objects.Add(item.gameObject);
            }
            objects = _objects;
            objects.RemoveAt(0);
        }
    }

    void OnDrawGizmos()
    {
        Vector3 pos = Camera.current.transform.position;
        Gizmos.color = this.color;

        for (float y = pos.y - offSet; y < pos.y + offSet; y += this.height)
        {
            var aux = Mathf.Floor(y / this.height) * this.height;
            Gizmos.DrawLine(new Vector3(-offSet, aux, 0),new Vector3(offSet, aux, 0));
        }

        for (float x = pos.x - offSet; x < pos.x + offSet; x += this.width)
        {
            var aux = Mathf.Floor(x / this.width) * this.width;
            Gizmos.DrawLine(new Vector3(aux, -offSet, 0), new Vector3(aux, offSet, 0));
        }
    }
}