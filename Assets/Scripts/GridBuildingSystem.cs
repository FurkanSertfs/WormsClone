using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridBuildingSystem : MonoBehaviour
{

    public static GridBuildingSystem Instance { get; private set; }



 
    public GridXY<GridObject> grid;

    private GridObject startNode, endNode;

    [SerializeField]
    GameObject player;



    private void Awake()
    {
        Instance = this;

        int gridWidth = 100;
        int gridHeight = 100;

        grid = new GridXY<GridObject>(gridWidth, gridHeight, new Vector3(0, 0, 0), (GridXY<GridObject> g, int x, int y) => new GridObject(g, x, y));


    }

    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        grid.GetXY(worldPosition, out int x, out int y);
        return new Vector2Int(x, y);
    }



    private void Update() 
    {
       // Debug.Log(GetGridPosition(player.transform.position));
    }

  


}

[System.Serializable]
public class GridObject
{
    private GridXY<GridObject> grid;
    public int x;
    public int y;

   
    public bool isHead;

    public int wormID=101;

    public GameObject food;


    public GridObject(GridXY<GridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;


    }



}


