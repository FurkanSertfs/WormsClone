using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridXY<TGridObject>
{

    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private int width;
    private int height;
   
    private Vector3 originPosition;
    public TGridObject[,] gridArray;
    public TextMesh[,] debugTextArray;
    public GridXY(int width, int height , Vector3 originPosition, Func<GridXY<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
       
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject(this, x, y);
            }
        }

    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

  

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y, 0)  + originPosition;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x);
        y = Mathf.FloorToInt((worldPosition - originPosition).y);
    }

   

    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(TGridObject);
        }
    }

    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x,y);
    }

    public Vector2Int ValidateGridPosition(Vector2Int gridPosition)
    {
        return new Vector2Int(
            Mathf.Clamp(gridPosition.x, 0, width - 1),
            Mathf.Clamp(gridPosition.y, 0, height - 1)
        );
    }

}
