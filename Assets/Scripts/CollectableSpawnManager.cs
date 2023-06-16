using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawnManager : MonoBehaviour
{
    public static CollectableSpawnManager instance;
    public GameObject[] foodPrefabs;

    [SerializeField]
    GameObject magnedPrefab;
    public int foodCount = 1000;
    public int minClusterSize = 4;
    public int maxClusterSize = 10;
    public float minDistanceBetweenFoods = 0.5f;

    int tryCount = 0;


    [SerializeField]
    private List<Vector2> spawnedCollectablePositions = new List<Vector2>();

    [SerializeField]
    Transform foodParent;

    Camera mainCamera;

    Vector2 clusterCenter;

    List<Transform> magnetList = new List<Transform>();

    private void OnEnable()
    {
        GameEvents.instance.OnFoodDestroyed += DestroyedFood;
    }

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

    void DestroyedFood(Vector2 spawnPoint)
    {
        spawnedCollectablePositions.Remove(spawnPoint);
        SpawnFoods();
    }

    void Start()
    {
        mainCamera = Camera.main;
        SpawnFoods();
        StartCoroutine(SpawnMagnet());
    }



    bool IsInCameraBounds(Vector2 point, Vector3 min, Vector3 max)
    {
        return point.x >= min.x && point.x <= max.x && point.y >= min.y && point.y <= max.y;
    }

    public void SpawnFood(Vector2 spawnPosition)
    {
        float foodSize = Random.Range(3f, 5.75f);
        int foodIndex = Random.Range(0, foodPrefabs.Length);
        GameObject newFood = Instantiate(foodPrefabs[foodIndex], spawnPosition, Quaternion.identity);
        newFood.transform.parent = foodParent;
        newFood.transform.localScale = Vector3.one * foodSize;
        spawnedCollectablePositions.Add(spawnPosition);

    }

    public IEnumerator SpawnMagnet()
    {
        while (true)
        {
            if (magnetList.Count < 5)
            {
                Vector3 cameraMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
                Vector3 cameraMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
                Vector2 spawnPosition = new Vector2(Random.Range(5, 95), Random.Range(5, 95));

                while (IsInCameraBounds(spawnPosition, cameraMin, cameraMax))
                {
                    spawnPosition = new Vector2(Random.Range(5, 95), Random.Range(5, 95));
                }
                GameObject newBuff = Instantiate(magnedPrefab, spawnPosition, Quaternion.identity);
                newBuff.transform.parent = foodParent;
                spawnedCollectablePositions.Add(spawnPosition);
                magnetList.Add(newBuff.transform);

            }

            for (int i = 0; i < magnetList.Count; i++)
            {
                if(magnetList[i]==null)
                {
                    magnetList.RemoveAt(i);

                }
                
            }

            yield return new WaitForSeconds(5f);

        }





    }

    void SpawnFoods()
    {
        int spawnedFoodCount = spawnedCollectablePositions.Count;

        Vector3 cameraMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 cameraMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        while (spawnedFoodCount < foodCount)
        {
            int clusterSize = Random.Range(minClusterSize, maxClusterSize + 1);
            clusterCenter = new Vector2(Random.Range(5, 95), Random.Range(5, 95));

            while (IsInCameraBounds(clusterCenter, cameraMin, cameraMax))
            {
                clusterCenter = new Vector2(Random.Range(5, 95), Random.Range(5, 95));
            }

            for (int i = 0; i < clusterSize; i++)
            {
                if (spawnedFoodCount >= foodCount)
                {
                    break;
                }

                float foodSize = Random.Range(3f, 5.75f);
                Vector2 spawnPosition = GetRandomSpawnPosition(clusterCenter, minDistanceBetweenFoods);

                if (spawnPosition != Vector2.zero)
                {
                    int foodIndex = Random.Range(0, foodPrefabs.Length);
                    GameObject newFood = Instantiate(foodPrefabs[foodIndex], spawnPosition, Quaternion.identity);
                    newFood.transform.parent = foodParent;
                    newFood.transform.localScale = Vector3.one * foodSize;
                    spawnedCollectablePositions.Add(spawnPosition);

                }

                spawnedFoodCount++;
                tryCount++;
            }
        }

        if (spawnedCollectablePositions.Count < foodCount)
        {
            SpawnFoods();
        }
    }

    Vector2 GetRandomSpawnPosition(Vector2 center, float minDistance)
    {

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(minDistance, minDistance * 2);
        Vector2 spawnPosition = center + randomDirection * randomDistance;

        bool hasOverlap = false;

        foreach (Vector2 existingPosition in spawnedCollectablePositions)
        {
            float distance = Vector2.Distance(existingPosition, spawnPosition);
            if (distance < minDistanceBetweenFoods)
            {
                hasOverlap = true;
                break;
            }
        }

        if (!hasOverlap)
        {
            return spawnPosition;
        }


        return Vector2.zero;
    }
}
public class FoodCircleCollider
{
    public Vector2 center;
    public float radius;

    public bool isTaked;

    public GameObject food;

    public FoodCircleCollider(float radius, GameObject food)
    {
        this.radius = radius;
        this.food = food;
    }
}