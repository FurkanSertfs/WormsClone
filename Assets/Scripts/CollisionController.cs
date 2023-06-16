using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public static CollisionController instance;
    public List<Vector2> squares = new List<Vector2>();

    public int diagonalLineLength;

    [SerializeField]
    int areaCount;




    public Dictionary<int, WormsInSquare> squareWormsDictionary = new Dictionary<int, WormsInSquare>();
    public Dictionary<int, FoodInSquare> squareFoodDictionary = new Dictionary<int, FoodInSquare>();

    public Dictionary<int, BuffInSquare> squareBuffDictionary = new Dictionary<int, BuffInSquare>();




    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {

        areaCount = 16;
        CreateSquares();

        for (int i = 0; i < squares.Count; i++)
        {
            squareWormsDictionary.Add(i, new WormsInSquare());
            squareFoodDictionary.Add(i, new FoodInSquare());
            squareBuffDictionary.Add(i, new BuffInSquare());
        }

        StartCoroutine(ClearTheList());
      


    }


    IEnumerator ClearTheList()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            for (int i = 0; i < squares.Count; i++)
            {
                squareWormsDictionary[i].CircleColliderCenterList.Clear();
               
            }
        }
      
    }


    void CreateSquares()
    {
        int sideLength = (int)Math.Sqrt(10000 / areaCount);
        int squareCount = 100 / sideLength;

        diagonalLineLength = (int)Math.Sqrt(2 * (sideLength * sideLength));




        Vector2 firstSquare = new Vector2(sideLength / 2, sideLength / 2);



        for (int i = 0; i < squareCount; i++)
        {

            Vector2 newSquareY = new Vector2(firstSquare.x, firstSquare.y + (sideLength * i));

            squares.Add(newSquareY);

            for (int j = 1; j < squareCount; j++)
            {
                Vector2 newSquareX = new Vector2(newSquareY.x + (sideLength * j), newSquareY.y);
                squares.Add(newSquareX);

            }

        }

    }



}

public class FoodInSquare
{
    public List<FoodCircleCollider> CircleColliderCenterList = new List<FoodCircleCollider>();


}
public class BuffInSquare
{
    public List<FoodCircleCollider> CircleColliderCenterList = new List<FoodCircleCollider>();


}

public class WormsInSquare
{
    public List<WormsCircleCollider> CircleColliderCenterList = new List<WormsCircleCollider>();


}
