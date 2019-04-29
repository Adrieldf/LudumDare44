using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    public static EnemiesSpawner Instance;
    public List<GameObject> Enemies;
    public List<GameObject> Spawners;
    private int waveEnemiesCount = 5;
    private int waveEnemiesSpawnLeft = 5;
    private bool waveOnGoing = false;
    private int currentWave = 0;
    private int waveEnemiesAlive = 0;
    private int enemiesPerWave => currentWave * waveEnemiesCount;
    public void EnemyKilled() => waveEnemiesAlive--;
    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        if (!waveOnGoing)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartWave();
            }
        }
        if (waveOnGoing && waveEnemiesAlive <= 0)
        {
            FinishWave();
        }
    }
    public void FinishWave()
    {
        //chamar os upgrades
        waveOnGoing = false;
    }
    public void StartWave()
    {
        currentWave++;
        waveEnemiesSpawnLeft = enemiesPerWave;
        waveEnemiesAlive = enemiesPerWave;
        waveOnGoing = true;
        StartCoroutine(SpawnWait());
    }

    public void SpawnEnemy()
    {
        if (waveEnemiesSpawnLeft > 0)
        {
            int val = Random.Range(0, Spawners.Count);
            Instantiate(Enemies[Random.Range(0, Enemies.Count)], Spawners[val].transform.position, Spawners[val].transform.rotation);
            waveEnemiesSpawnLeft--;
        }
    }
    IEnumerator SpawnWait()
    {
        while (waveEnemiesSpawnLeft > 0)
        {
            yield return new WaitForSeconds(1f);
            SpawnEnemy();
        }
    }
}
