using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUpdater : MonoBehaviour
{
    Vector2Int position;
    [SerializeField]
    bool isHead;
    bool isTail;

    public int wormID;


    private void FixedUpdate()
    {
        UpdateGridPosition();
    }

    void UpdateGridPosition()
    {
        if(isTail)
        {
            GridBuildingSystem.Instance.grid.GetGridObject(position.x, position.y).wormID = 101;

        }

        position = GridBuildingSystem.Instance.GetGridPosition(transform.position);

        GridObject gridObject =GridBuildingSystem.Instance.grid.GetGridObject(position.x, position.y);
        ControlCollision(gridObject);
        ControlFood(gridObject);


    }
    void ControlCollision(GridObject gridObject)
    {
        if (gridObject.wormID !=wormID && !gridObject.isHead)
        {
            if(gridObject.wormID !=101)
            {
                Debug.Log("Game Over " + gridObject.wormID + " " + wormID);

            }
        }


        else
        {
            gridObject.wormID = wormID;
        }


    }


    void ControlFood(GridObject gridObject)
    {
        int radius = 2;

        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {

                if (GridBuildingSystem.Instance.grid.GetGridObject(gridObject.x + x, gridObject.y + y).food != null)
                {
                    Debug.Log("Food");
                    
                    Destroy(GridBuildingSystem.Instance.grid.GetGridObject(gridObject.x + x, gridObject.y + y).food);

                    //invoke food destroyed event
                    GameEvents.instance.OnFoodDestroyed?.Invoke();

                 




                    
                    

                }
            }
        }
    }





}

