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

    private int score = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Update()
    {
        for (int i = 0; i < enemyChannels.Length; i++)
        {
            EnemyChannel channel = enemyChannels[i];
            if (!channel.HasEnemy)
            {
                Enemy enemy = Instantiate(channel.enemyPrefab, channel.spawnPosition.position, Quaternion.identity);
                enemy.ChannelIndex = i;
                enemy.movePositions = channel.movePositions;
                enemy.OnSunk.AddListener(OnEnemySunk);
                channel.HasEnemy = true;
            }
        }
    }

    public void OnEnemySunk(int channel)
    {
        score += scoreOnKill;
        scoreText.text = "Score: " + score;
        enemyChannels[channel].HasEnemy = false;
    }

    [System.Serializable]
    private class EnemyChannel
    {
        public Enemy enemyPrefab;
        public Transform[] movePositions;
        public Transform spawnPosition;

        public bool HasEnemy { get; set; }
    }
}
