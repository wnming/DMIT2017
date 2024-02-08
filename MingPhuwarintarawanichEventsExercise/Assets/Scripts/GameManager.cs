using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour
{
    public int Score;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject startPanel;
    [SerializeField] TextMeshProUGUI startButtonText;
    [SerializeField] TextMeshProUGUI startScoreText;
    [SerializeField] TextMeshProUGUI timerText;

    [SerializeField] Coin[] coins;
    private float spawnCounter;

    private float timeLimit;

    void Start()
    {
        MyEvents.CollectCoin.AddListener(CollectCoin);
        MyEvents.SpawnCoin.AddListener(SpawnCoins);

        startScoreText.text = "";
        startButtonText.text = "Start";

        ResetValues();
    }

    void Update()
    {
        if (!startPanel.activeSelf && timeLimit > 0)
        {
            if (spawnCounter > 0)
            {
                spawnCounter -= Time.deltaTime;
            }
            else
            {
                MyEvents.SpawnCoin.Invoke();
                spawnCounter = 2.0f;
            }
            scoreText.text = Score.ToString();
            timerText.text = timeLimit.ToString("0");
            timeLimit -= Time.deltaTime;
        }
        else
        {
            if (timeLimit <= 0)
            {
                //end game
                GameObject[] gosArray = GameObject.FindGameObjectsWithTag("Coin");
                foreach (GameObject coin in gosArray)
                {
                    Destroy(coin);
                }

                startScoreText.text = $"Score(s): {Score.ToString()}";
                startButtonText.text = "Play Again";
                startPanel.SetActive(true);

                Time.timeScale = 0;
            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void ResetValues()
    {
        Time.timeScale = 1;
        timeLimit = 10.0f;

        spawnCounter = 2;
        Score = 0;
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        ResetValues();
    }

    public void SpawnCoins()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-8.0f, 8.0f), 1, Random.Range(-8.0f, 8.0f));
        int randomCoinIndex = Random.Range(0, coins.Length);
        Instantiate(coins[randomCoinIndex], spawnPosition, coins[randomCoinIndex].transform.rotation);
    }

    public void CollectCoin(int value)
    {
        Score += value;
        if(Score < 0)
        {
            Score = 0;
        }
    }

}
