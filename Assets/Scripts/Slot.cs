using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Slot : MonoBehaviour
{
    #region var
    public bool visited;

    public List<Slot> links;
    public List<Slot> walkableNext;

    public GameObject wallTop;
    public GameObject wallBot;
    public GameObject wallLeft;
    public GameObject wallRight;
    #endregion

    void Awake()
    {
        //wallTop = transform.Find("wallTop").gameObject;
        //wallBot = transform.Find("wallBot").gameObject;
        //wallRight = transform.Find("wallRight").gameObject;
        //wallLeft = transform.Find("wallLeft").gameObject;
    }

    public void RemoveWalls(Slot b)
    {
        Transform tA = GetComponent<Transform>();
        Transform tB = b.GetComponent<Transform>();
        
        if (tA.position.x > tB.position.x)
        {
            DestroyImmediate(wallLeft);
            DestroyImmediate(b.wallRight);
         //   Debug.Log("A");
        }
        else if (tA.position.x < tB.position.x)
        {
            DestroyImmediate(b.wallLeft);
            DestroyImmediate(wallRight);
         //   Debug.Log("B");
        }

        else if (tA.position.z < tB.position.z)
        {
            DestroyImmediate(b.wallBot);
            DestroyImmediate(wallTop);
        //    Debug.Log("C");
        }
        else if (tA.position.z > tB.position.z)
        {
            DestroyImmediate(wallBot);
            DestroyImmediate(b.wallTop);
        //    Debug.Log("D");
        }
    }
}
