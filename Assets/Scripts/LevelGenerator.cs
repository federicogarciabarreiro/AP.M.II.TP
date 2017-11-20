using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LevelGenerator : MonoBehaviour
{
    #region var 
    public GameObject slot;

    public Material wallMaterial, baseMaterial;

    public GameObject level;

    public static List<GameObject> maze = new List<GameObject>();
    public List<GameObject> neighbourList;

    public int width;
    public int height;

    int[,] _level;
    #endregion

    public void Init()
    {
        if (width > 0 && height > 0)
        {
            maze = new List<GameObject>();
            level = new GameObject("Level");
            level.transform.parent = this.transform;

            _level = new int[width, height];
            GenerateMatriz();
        }

    }

    public void LevelDestroy()
    {
        if (level != null) DestroyImmediate(level);
    }

    public void InitAndDestroy()
    {
        LevelDestroy();
        Init();
    }

    public void GenerateMatriz()
    {
        for (int i = _level.GetLength(0) - 1; i >= 0; i--)
        {
            for (int j = _level.GetLength(1) - 1; j >= 0; j--)
            {
                var _slot = Instantiate(slot, new Vector3(i, 0, j), Quaternion.identity);
                _slot.transform.parent = level.transform;
                maze.Add(_slot);
            }
        }

        GetNeighbours();
       
    }
    
    public void GetNeighbours()
    {
        foreach (var sl in maze)
        {
            foreach (Collider col in Physics.OverlapSphere(sl.transform.position, 0.5f))
            {
                if (col.tag == "Slot")
                {
                    if (sl.GetComponent<Slot>().links == null) sl.GetComponent<Slot>().links = new List<Slot>();
                    if (sl != col.gameObject) sl.GetComponent<Slot>().links.Add(col.gameObject.GetComponent<Slot>());
                }
            }
        }
        DeepFirstSearch(maze[0].GetComponent<Slot>());

    }

    public void DeepFirstSearch(Slot start)
    { 
        Stack<Slot> S = new Stack<Slot>();

        S.Push(start);
        start.visited = true;

        while (S.Count > 0)
        {
            Slot V = S.Peek();

            List<Slot> notVisitedSlots = new List<Slot>();

            foreach (var item in V.links)
            {
                if (!item.visited) notVisitedSlots.Add(item);
            }

            if (notVisitedSlots.Count > 0)
            {
                Slot W = notVisitedSlots[Random.Range(0, notVisitedSlots.Count)];

                S.Push(W);
                V.RemoveWalls(W);
                V.walkableNext.Add(W);

                W.visited = true;
            }
            else {
                S.Pop();
            }
        }
    }
    }