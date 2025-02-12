using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave : System.IComparable<Wave>
    {
        public int waveNumber;
        public int enemyCount;
        public float spawnDelay;
        public int priority;

        public int CompareTo(Wave other)
        {
            return priority.CompareTo(other.priority);
        }
    }

    [SerializeField] private Transform enemyPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private TMP_Text waveText;
    private PriorityQueue<Wave> waveQueue;
    private float countdown = 2f;

    private void Start()
    {
        InitializeWaves();
    }

    private void InitializeWaves()
    {
        waveQueue = new PriorityQueue<Wave>();

        // Create initial waves with increasing priority
        for (int i = 1; i <= 5; i++)
        {
            Wave wave = new Wave
            {
                waveNumber = i,
                enemyCount = i * 2,
                spawnDelay = 0.5f,
                priority = i
            };
            waveQueue.Enqueue(wave);
        }
    }

    private void Update()
    {
        if (waveQueue.Count == 0) return;

        countdown -= Time.deltaTime;
        waveText.text = $"Next Wave: {Mathf.Max(0, Mathf.Round(countdown))}";

        if (countdown <= 0)
        {
            Wave currentWave = waveQueue.Dequeue();
            StartCoroutine(SpawnWave(currentWave));
            countdown = 5f;
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(wave.spawnDelay);
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}