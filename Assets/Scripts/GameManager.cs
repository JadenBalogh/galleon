using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] private int scoreOnKill = 20;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private EnemyChannel[] enemyChannels;
    [SerializeField] private Transform[] spawnablePoints;
    [SerializeField] private GameObject[] spawnablePrefabs;
    [SerializeField] private float minSpawnableInterval = 2f;
    [SerializeField] private float maxSpawnableInterval = 8f;

    public static bool IsStarted { get; private set; }

    private int score = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        IsStarted = false;
    }

    private void Start()
    {
        StartCoroutine(SpawnableLoop());
    }

    private void Update()
    {
        if (!IsStarted)
        {
            return;
        }

        for (int i = 0; i < enemyChannels.Length; i++)
        {
            EnemyChannel channel = enemyChannels[i];
            if (!channel.HasEnemy)
            {
                Debug.Log(channel.enemyPrefabs.Length);
                Enemy enemyPrefab = channel.enemyPrefabs[Random.Range(0, channel.enemyPrefabs.Length)];
                Debug.Log(enemyPrefab.name);
                Enemy enemy = Instantiate(enemyPrefab, channel.spawnPosition.position, Quaternion.identity);
                enemy.ChannelIndex = i;
                enemy.movePositions = channel.movePositions;
                enemy.OnSunk.AddListener(OnEnemySunk);
                channel.HasEnemy = true;
            }
        }
    }

    private IEnumerator SpawnableLoop()
    {
        while (true)
        {
            Transform spawnablePoint = spawnablePoints[Random.Range(0, spawnablePoints.Length)];
            GameObject spawnablePrefab = spawnablePrefabs[Random.Range(0, spawnablePrefabs.Length)];
            Instantiate(spawnablePrefab, spawnablePoint.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(minSpawnableInterval, maxSpawnableInterval));
        }
    }

    public static void AddScore(int extraScore)
    {
        instance.score += extraScore;
        instance.scoreText.text = "Score: " + instance.score;
    }

    public static void StartGame()
    {
        IsStarted = true;
    }

    public void OnEnemySunk(int channel)
    {
        AddScore(scoreOnKill);
        enemyChannels[channel].HasEnemy = false;
    }

    [System.Serializable]
    private class EnemyChannel
    {
        public Enemy[] enemyPrefabs;
        public Transform[] movePositions;
        public Transform spawnPosition;

        public bool HasEnemy { get; set; }
    }
}
