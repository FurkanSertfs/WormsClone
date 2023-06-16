using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject[] worms;

    [SerializeField]

    Sprite[] wormsImages;

    [SerializeField]
    Image profileImage;

    public int wormsCount;

    [SerializeField]
    Text modNameText;

    [SerializeField]
    Sprite[] modNameImages;
    [SerializeField]
    Image modImage;

    int state;
    GameObject worm;


    int score;
    int totalDestroyedWorms;
    int highScore;
    int bestGameTime;
    int bestDestroyedWorms;
    int totalWins;
    int totalLoses;
    float totalSprintsTime;

    float gameTime;

    [SerializeField]
    Transform statisticsParent;
    [SerializeField]
    UIStatisticsElement statisticsPrefab;

    List<UIStatisticsElement> createdStatistics = new List<UIStatisticsElement>();

    List<Statistic> statistics = new List<Statistic>();


    private void Awake()
    {
        wormsCount = Random.Range(0, worms.Length);
        worm = Instantiate(worms[wormsCount], transform);
        profileImage.sprite = wormsImages[wormsCount];
        LoadStatistics();


    }
    public void ClearStatistics()
    {
        PlayerPrefs.DeleteAll();

        foreach (UIStatisticsElement stat in createdStatistics)
        {
            stat.scoreText.text = "0";
        }
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
        CreateStatisticsElemnts();
    }

    void CreateStatisticsElemnts()
    {
       

        statistics.Add(new Statistic("En Yüksek Skor", highScore));
        statistics.Add(new Statistic("En Uzun Oyun Süresi", bestGameTime));
        statistics.Add(new Statistic("En Fazla Yok Edilen", bestDestroyedWorms));
        statistics.Add(new Statistic("Yok Edilen Kurtlar", totalDestroyedWorms));
        statistics.Add(new Statistic("Toplam Kazanma", totalWins));
        statistics.Add(new Statistic("Toplam Kaybetme", totalLoses));
        statistics.Add(new Statistic("Toplam Hızlı Gitme Süresi", (int)totalSprintsTime));



        foreach (Statistic stat in statistics)
        {
            UIStatisticsElement statElement = Instantiate(statisticsPrefab, statisticsParent);
            createdStatistics.Add(statElement);
            statElement.statisticsNameText.text = stat.Name;
            statElement.scoreText.text = stat.Value.ToString();
        }

    }

    public void OpenScene()
    {
        PlayerPrefs.SetInt("character", wormsCount);

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }






    public void ChangeWorm()
    {
        Destroy(worm);
        wormsCount++;
        if (wormsCount == worms.Length)
        {
            wormsCount = 0;
        }
        worm = Instantiate(worms[wormsCount], transform);
        profileImage.sprite = wormsImages[wormsCount];
    }

    public void ChangeMod()
    {

        if (state == 1)
        {
            modImage.sprite = modNameImages[0];
            modNameText.text = "Klasik";
            modNameText.color = Color.white;
            state = 0;
        }

        else
        {
            modImage.sprite = modNameImages[1];
            modNameText.text = "Arena";
            modNameText.color = Color.red;
            state = 1;


        }


    }
}
[System.Serializable]
public class Statistic
{
    public string Name { get; set; }
    public int Value { get; set; }

    public Statistic(string name, int value)
    {
        Name = name;
        Value = value;
    }
}

