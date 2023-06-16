using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



public class WormController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 100f;
    public Transform controlStick;

    CollisionController collisionController;

    [SerializeField]
    public List<BodyPartController> bodyParts;


    WormsCircleCollider circleCollider;

    [SerializeField]
    List<int> controlledWorms = new List<int>();
    public float bodyRadius = 1f;



    [SerializeField]
    List<int> wormsInSquare = new List<int>();

    public int wormID;

    [SerializeField]
    bool isPlayer;

    [SerializeField]
    EnemyController enemyController;

    Vector2 leftWall = new Vector2(-1, 0);

    [SerializeField]
    Transform parentCharacter;

    Camera mainCamera;

    [SerializeField]
    GameObject endScreen;


    int collectRange = 2;
    float initialGrowthThreshold = 3;
    float growthThresholdIncreaseRate = 1.75f;
    float comboTimeLimit = 0.25f;
    int baseScorePerFood = 14;
    float scoreMultiplierRate = 2.25f;

    private int foodEaten;
    private float growthThreshold = 3;
    private float timeSinceLastFood;
    private int comboCount;

    bool isBuffActive;

    float buffTime;

    [SerializeField]
    Image buffBar;

    [SerializeField]
    GameObject buffBarParent;

    int destroyedWorms;
  


    





    private void Start()
    {
        collisionController = CollisionController.instance;
        circleCollider = new WormsCircleCollider(wormID);
        mainCamera = Camera.main;


        UpdateCollider();

        StartCoroutine(ControlCollsionRoutine());



    }

    private void Update()
    {


    }



    IEnumerator ControlCollsionRoutine()
    {
        while (true)
        {
            UpdateTheColliderCenterPosition();
            UpdateTheSquare();
            ControlCollisionsWorms();
            ControlCollisionFoods();
            ControlCollisionWall();
            if (isPlayer)
            {
                ControlCollisionBuffs();
            }
            yield return new WaitForSeconds(0.1f);
            controlledWorms.Clear();
        }

    }



    void UpdateTheColliderCenterPosition()
    {
        if (bodyParts.Count % 2 == 0)
        {
            circleCollider.center = bodyParts[(bodyParts.Count / 2) - 1].transform.position;
        }

        else
        {
            circleCollider.center = (bodyParts[bodyParts.Count / 2].transform.position + bodyParts[(bodyParts.Count / 2) - 1].transform.position) / 2f;
        }

    }

    void UpdateCollider()
    {
        if (bodyParts.Count > 0)
        {
            UpdateTheColliderCenterPosition();

            float radius = bodyRadius / 2;

            //  circleCollider.radius = Vector2.Distance(transform.position, circleCollider.center) + radius;

            circleCollider.radius = radius * (bodyParts.Count + 1);


        }





    }





    void UpdateTheSquare()
    {



        List<int> tempWormsInSquare = new List<int>();
        for (int i = 0; i < collisionController.squares.Count; i++)
        {
            if (Vector2.SqrMagnitude(circleCollider.center - collisionController.squares[i]) < Mathf.Pow(circleCollider.radius + collisionController.diagonalLineLength / 2f, 2))

            {


                if (!collisionController.squareWormsDictionary[i].CircleColliderCenterList.Contains(circleCollider))
                {
                    collisionController.squareWormsDictionary[i].CircleColliderCenterList.Add(circleCollider);

                }

                tempWormsInSquare.Add(i);


            }

        }




        wormsInSquare = tempWormsInSquare;


    }



    void ControlCollisionWall()
    {
        if (!isPlayer)
        {
            if (IsOutsideWall())
            {
                DestroyTheWorm();
                enemyController.RotateDirection(true);
            }
        }
        else
        {
            if (IsOutsideWall())
            {
                EndTheGame();
            }
        }
    }

    bool IsOutsideWall()
    {
        Vector3 currentPosition = transform.position;
        Vector3 lastBodyPartPosition = bodyParts[bodyParts.Count - 1].transform.position;

        bool isOutsideX = currentPosition.x >= 100 || lastBodyPartPosition.x >= 100 || currentPosition.x <= 0 || lastBodyPartPosition.x <= 0;
        bool isOutsideY = currentPosition.y >= 100 || lastBodyPartPosition.y >= 100 || currentPosition.y <= 0 || lastBodyPartPosition.y <= 0;

        return isOutsideX || isOutsideY;
    }




    public void EndTheGame()
    {
        
        GameManager.instance.SaveStatistics(destroyedWorms);
        endScreen.SetActive(true);





    }

  
    


    void ControlCollisionFoods()
    {
        for (int i = 0; i < wormsInSquare.Count; i++)
        {
            FoodInSquare squareFoodsDictionary = collisionController.squareFoodDictionary[wormsInSquare[i]];
            List<FoodCircleCollider> foodsToRemove = new List<FoodCircleCollider>();

            for (int j = 0; j < squareFoodsDictionary.CircleColliderCenterList.Count; j++)
            {
                FoodCircleCollider foodCollider = squareFoodsDictionary.CircleColliderCenterList[j];

                // Eğer yiyecek yok edilmişse, listeden kaldır
                if (foodCollider.food == null)
                {
                    foodsToRemove.Add(foodCollider);
                    foodCollider.isTaked = true;
                    continue;
                }

                if (Vector2.SqrMagnitude(circleCollider.center - foodCollider.center) < Mathf.Pow(circleCollider.radius + (3), 2))
                {
                    Transform food = foodCollider.food.transform;



                    if (Vector2.Distance(transform.position, food.transform.position) < collectRange + (bodyRadius))
                    {
                        GameEvents.instance.OnFoodDestroyed?.Invoke(food.transform.position);
                        food.GetComponent<Food>().FollowTheWorm(transform);
                        foodsToRemove.Add(foodCollider);
                      
                        continue;
                    }

                    
                }
            }

            foreach (FoodCircleCollider foodToRemove in foodsToRemove)
            {
                squareFoodsDictionary.CircleColliderCenterList.Remove(foodToRemove);
                Grow();

                if (isPlayer)
                {
                    if( !foodToRemove.isTaked)
                    {
                        IncreaseScoreWithCombo();
                        foodToRemove.isTaked = true;
                    }
                   

                  

                }
            }

        }
    }


    void ControlCollisionBuffs()
    {
        for (int i = 0; i < wormsInSquare.Count; i++)
        {
            BuffInSquare squareBuffssDictionary = collisionController.squareBuffDictionary[wormsInSquare[i]];
            List<FoodCircleCollider> foodsToRemove = new List<FoodCircleCollider>();

            for (int j = 0; j < squareBuffssDictionary.CircleColliderCenterList.Count; j++)
            {
                FoodCircleCollider foodCollider = squareBuffssDictionary.CircleColliderCenterList[j];


                if (foodCollider.food == null)
                {
                    foodsToRemove.Add(foodCollider);
                    continue;
                }

                if (Vector2.SqrMagnitude(circleCollider.center - foodCollider.center) < Mathf.Pow(circleCollider.radius + (3), 2))
                {
                    Transform food = foodCollider.food.transform;



                    if (Vector2.Distance(transform.position, food.transform.position) < collectRange + (bodyRadius))
                    {
                        GameEvents.instance.OnFoodDestroyed?.Invoke(food.transform.position);
                        food.GetComponent<Buff>().FollowTheWorm(transform);
                        foodsToRemove.Add(foodCollider);
                        continue;
                    }
                    else
                    {

                    }
                }
            }

            foreach (FoodCircleCollider foodToRemove in foodsToRemove)
            {
                squareBuffssDictionary.CircleColliderCenterList.Remove(foodToRemove);


                IncreaseRange();


            }

        }
    }


    void IncreaseRange()
    {
        buffTime = 10;
        if (!isBuffActive)
        {
            buffBarParent.SetActive(true);
            StartCoroutine(BuffTimer());
        }




    }

    IEnumerator BuffTimer()
    {
        while (buffTime > 0)
        {
            buffTime -= Time.deltaTime;
            collectRange = 4;
            yield return null;
            buffBar.fillAmount = buffTime / 10;
        }
        collectRange = 2;

        isBuffActive = false;
        buffBarParent.SetActive(false);

    }


    void ControlCollisionsWorms()
    {

        for (int i = 0; i < wormsInSquare.Count; i++)
        {
            WormsInSquare squareWormsDictionary = collisionController.squareWormsDictionary[wormsInSquare[i]];


            for (int j = 0; j < squareWormsDictionary.CircleColliderCenterList.Count; j++)
            {


                if (wormID == squareWormsDictionary.CircleColliderCenterList[j].wormsID)
                {

                    continue;
                }

                if (controlledWorms.Contains(squareWormsDictionary.CircleColliderCenterList[j].wormsID))
                {
                    continue;
                }

                WormController otherWorms = GameManager.instance.wormControllers[squareWormsDictionary.CircleColliderCenterList[j].wormsID];

                if (Vector2.SqrMagnitude(circleCollider.center - squareWormsDictionary.CircleColliderCenterList[j].center) < Mathf.Pow(circleCollider.radius + squareWormsDictionary.CircleColliderCenterList[j].radius, 2))
                {
                    enemyController.RotateDirection(true);

                    if (Vector2.Distance(transform.position, otherWorms.transform.position) < bodyRadius)
                    {
                        DestroyTheWorm();
                        destroyedWorms++;
                        otherWorms.destroyedWorms++;
                        otherWorms.DestroyTheWorm();



                    }

                    for (int l = 0; l < bodyParts.Count; l++)
                    {
                        if (Vector2.Distance(bodyParts[l].transform.position, otherWorms.transform.position) < bodyRadius)
                        {
                            otherWorms.DestroyTheWorm();
                            destroyedWorms++;
                            return;
                        }




                    }

                    for (int k = 0; k < otherWorms.bodyParts.Count; k++)
                    {

                        if (Vector2.Distance(transform.position, otherWorms.bodyParts[k].transform.position) < bodyRadius)
                        {
                            DestroyTheWorm();
                            otherWorms.destroyedWorms++;
                            return;
                        }




                    }

                }

                else
                {



                    if (!otherWorms.controlledWorms.Contains(wormID))
                    {
                        otherWorms.controlledWorms.Add(wormID);


                    }


                }

                //Debug.Log("Circle Raid: " + circleCollider.radius + " Other radius: " + collisionController.squareDictionary[wormsInSquare[i]].CircleColliderCenterList[j].radius);
                // Debug.Log("SqrMagnitude = " + Vector2.SqrMagnitude(circleCollider.center - squareWormsDictionary.CircleColliderCenterList[j].center) + " Mathf.Pow: " + Math.Pow(circleCollider.radius + collisionController.squareWormsDictionary[wormsInSquare[i]].CircleColliderCenterList[j].radius, 2));
            }

        }


    }

    void Grow()
    {
        foodEaten++;



        if (foodEaten >= growthThreshold)
        {


            growthThreshold *= growthThresholdIncreaseRate;
            foodEaten = 0;
            CreateBodyPart(1);
            transform.localScale *= 1.0325f;
            bodyRadius *= 1.0325f;

            for (int i = 0; i < bodyParts.Count; i++)
            {

                bodyParts[i].maxDistance = bodyRadius;
                bodyParts[i].transform.localScale = transform.localScale;
            }

            UpdateCollider();
        }

    }



    void IncreaseScoreWithCombo()
    {

     
       
        int scoreToAdd = baseScorePerFood;

        if (Time.time - timeSinceLastFood < comboTimeLimit)
        {
            comboCount++;

            scoreToAdd = Mathf.RoundToInt(baseScorePerFood * Mathf.Min(12, (Mathf.Pow(comboCount, 1.01f) + 2 + Mathf.Sin(comboCount) / 3) / 2.75f));


        }
        else
        {
            comboCount = 0;
        }

        GameEvents.instance.OnUpdateScore?.Invoke(scoreToAdd);


        timeSinceLastFood = Time.time;
    }

    void CreateBodyPart(int addSize)
    {
        for (int i = 0; i < addSize; i++)
        {
            GameObject newBodyPart = Instantiate(bodyParts[0].gameObject, bodyParts[bodyParts.Count - 1].transform.position, bodyParts[bodyParts.Count - 1].transform.rotation);
            newBodyPart.transform.parent = parentCharacter;
            newBodyPart.GetComponent<BodyPartController>().target = bodyParts[bodyParts.Count - 1].transform;
            bodyParts.Add(newBodyPart.GetComponent<BodyPartController>());


        }

    }



    public void DestroyTheWorm()
    {
        if (!isPlayer)
        {
            int newSize = Random.Range(3, 6);

            CollectableSpawnManager.instance.SpawnFood(transform.position);

            for (int i = 0; i < bodyParts.Count; i++)
            {
                CollectableSpawnManager.instance.SpawnFood(bodyParts[i].transform.position);

            }

            if (bodyParts.Count > newSize)
            {
                for (int i = bodyParts.Count - 1; i >= newSize; i--)
                {
                    Destroy(bodyParts[i].gameObject);
                }

                for (int i = bodyParts.Count - 1; i >= newSize; i--)
                {
                    bodyParts.RemoveAt(i);
                }


            }

            else
            {
                CreateBodyPart(bodyParts.Count - newSize);
            }

            for (int i = 0; i < bodyParts.Count; i++)
            {
                bodyParts[i].transform.localPosition = Vector3.zero;
            }

            transform.localPosition = Vector3.zero;

            Vector3 cameraMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            Vector3 cameraMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

            Vector3 spawnPoint = new Vector3(Random.Range(5, 95), Random.Range(5, 95), 0);

            while (IsInCameraBounds(spawnPoint, cameraMin, cameraMax))
            {
                spawnPoint = new Vector3(Random.Range(5, 95), Random.Range(5, 95), 0);
               
            }

            parentCharacter.position = spawnPoint;
            enemyController.RotateDirection(false);
            UpdateCollider();

        }
        else
        {
            EndTheGame();
        }



    }


    bool IsInCameraBounds(Vector2 point, Vector3 min, Vector3 max)
    {
        return point.x >= min.x && point.x <= max.x && point.y >= min.y && point.y <= max.y;
    }



}

public class WormsCircleCollider
{
    public int wormsID;
    public Vector2 center;
    public float radius;

    public WormsCircleCollider(int wormsID)
    {
        this.wormsID = wormsID;

    }

}