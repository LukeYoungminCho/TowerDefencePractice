using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log(this.ToString() + " is already existed in the scene!");
            return;
        }
        instance = this;
    }

    public Transform enemyPrefab;
    public Transform spawnPoint;
    public float timeToStartFirstWave = 3f;
    public float timeBetweenWaves = 5f;
    public Text waveCountDownTimerText;
    private st_waves[] waves = new st_waves[9];
    public int numOfEnemies;
    private float countdown;    
    private int waveIndex = 0;
    private bool isGameOver = false;
    private PlayerStats playerStats;

    private void Start()
    {
        countdown = timeToStartFirstWave;
        playerStats = GetComponent<PlayerStats>();
    }
    private void Update()
    {
        if (isGameOver) return;

        if (waveIndex < 8)
        {
            WaveUpdate(timeBetweenWaves);
        }
        else
        {
            waveIndex = 0; // limitless loop for testing
        }
    }
    // return countDownstring. call in timely called task.
    void WaveUpdate(float timeDelayBetweenCounting)
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeDelayBetweenCounting;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountDownTimerText.text = string.Format("{0:00.00}", countdown); //Mathf.Round(countdown).ToString(); // round down.
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;

        if (waveIndex > 8)
        {
            yield return 0;
        }
        else
        {   
            Debug.Log("Wave Incoming!");

            for (int i = 0; i < waveIndex; i++)
            {
                SpawnEnemy();
                waves[waveIndex].count++;                
                yield return new WaitForSeconds(0.2f);
            }
            numOfEnemies = waves[waveIndex].count;
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
    
    struct st_waves
    {
        public int count;
    }
    
    public void GameOver()
    {
        // initiate time laps, money, lifes
        countdown = timeToStartFirstWave;
        waveCountDownTimerText.text = string.Format("{0:00.00}", countdown);
        PlayerStats.money = playerStats.startMoney;
        PlayerStats.lives = playerStats.startLives;
        isGameOver = true;
        DeleteAllGameElements();
    }

    public void DeleteAllGameElements()
    {        
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
        //gameObjects.Initialize(); // Q. 필요한가??
        gameObjects = GameObject.FindGameObjectsWithTag("TowerComponent");
        for (int i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
        //gameObjects.Initialize();
        gameObjects = GameObject.FindGameObjectsWithTag("Tower");
        for (int i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }      
    }

    public void RestartGame()
    {
        isGameOver = false;
    }
}
