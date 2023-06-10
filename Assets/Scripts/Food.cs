using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    Vector2Int position;
    private void Start() 
    {
        position= GridBuildingSystem.Instance.GetGridPosition(transform.position);

        GridBuildingSystem.Instance.grid.GetGridObject(position.x, position.y).food = gameObject;
    }
}
