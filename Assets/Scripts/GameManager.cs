using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // instance
    public static GameManager instance;

    public List<WormController> wormControllers;

    [SerializeField]
    GameObject[] wormsPrefabs;

    [SerializeField]
    int enemyCount;

    Camera mainCamera;

    [SerializeField]
    PlayerController playerController;

    int score;
    int totalDestroyedWorms;
    int highScore;
    int bestGameTime;
    int bestDestroyedWorms;
    int totalWins;
    int totalLoses;
    float totalSprintsTime;

    float gameTime;

    bool isSaved;


    public void SaveStatistics(int destroyedWorms)
    {
        if (!isSaved)
        {
            isSaved = true;
            score = playerController.score;
            PlayerPrefs.SetInt("skor", playerController.score);
            totalDestroyedWorms += destroyedWorms;

            if (bestGameTime < Time.time)
            {
                PlayerPrefs.SetInt("bestGameTime", (int)Time.time);

            }



            PlayerPrefs.SetInt("totalDestroyedWorms", totalDestroyedWorms);

            if (bestDestroyedWorms < destroyedWorms)
            {
                PlayerPrefs.SetInt("bestDestroyedWorms", destroyedWorms);
            }

            PlayerPrefs.SetInt("totalWins", totalWins);
            PlayerPrefs.SetInt("totalLoses", totalLoses);
            totalSprintsTime += playerController.sprintTime;
            PlayerPrefs.SetInt("totalSprintsTime", (int)totalSprintsTime);



            if (score > highScore)
            {
                PlayerPrefs.SetInt("highScore", score);

            }
        }





        PlayerPrefs.Save();

    }

    void LoadStatistics()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
        bestGameTime = PlayerPrefs.GetInt("bestGameTime", 0);
        bestDestroyedWorms = PlayerPrefs.GetInt("bestDestroyedWorms", 0);
        totalDestroyedWorms = PlayerPrefs.GetInt("totalDestroyedWorms", 0);
        totalWins = PlayerPrefs.GetInt("totalWins", 0);
        totalLoses = PlayerPrefs.GetInt("totalLoses", 0);
        totalSprintsTime = PlayerPrefs.GetInt("totalSprintsTime", 0);

    }

    public void OpenMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
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

        LoadStatistics();
    }

    private void Start()
    {

        for (int i = 0; i < enemyCount; i++)
        {

            Vector2 spawnPosition = new Vector2(Random.Range(5, 95), Random.Range(5, 95));

            while (IsInCameraBounds(spawnPosition, new Vector3(40, 40, 0), new Vector3(60, 60, 0)))
            {
                spawnPosition = new Vector2(Random.Range(5, 95), Random.Range(5, 95));

            }


            int wormsPrefabIndex = Random.Range(0, wormsPrefabs.Length);
            GameObject newEnemy = Instantiate(wormsPrefabs[wormsPrefabIndex], spawnPosition, Quaternion.identity);
            newEnemy.GetComponentInChildren<WormController>().wormID = i + 2;
            wormControllers.Add(newEnemy.GetComponentInChildren<WormController>());
        }
    }

    bool IsInCameraBounds(Vector2 point, Vector3 min, Vector3 max)
    {
        return point.x >= min.x && point.x <= max.x && point.y >= min.y && point.y <= max.y;
    }







}
