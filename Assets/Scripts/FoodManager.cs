using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField]
    GridBuildingSystem gridBuildingSystem;

    [SerializeField]
    GameObject[] foodList;

    [SerializeField]
    int startFoodCount;

    private void OnEnable()
    {
        // gameevents subscribe
        GameEvents.instance.OnFoodDestroyed += CreateFood;

    }

    private void Start()
    {

        for (int i = 0; i < startFoodCount; i++)
        {
            CreateFood();
        }
    }


    void CreateFood()
    {
        int x = Random.Range(0, 100);
        int y = Random.Range(0, 100);

        if (gridBuildingSystem.grid.GetGridObject(x, y).wormID == 101 && gridBuildingSystem.grid.GetGridObject(x, y).food == null)
        {
            int randomFood = Random.RandomRange(0, foodList.Length);
            gridBuildingSystem.grid.GetGridObject(x, y).food = Instantiate(foodList[randomFood], new Vector3(x, y, 0), Quaternion.identity) as GameObject;

        }
        else
        {
            CreateFood();
        }

    }
}
