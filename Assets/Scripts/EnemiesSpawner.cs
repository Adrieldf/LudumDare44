using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemiesSpawner : MonoBehaviour
{
    public static EnemiesSpawner Instance;
    public List<GameObject> Enemies;
    public List<GameObject> Spawners;
    private int waveEnemiesCount = 5;
    private int waveEnemiesSpawnLeft = 5;
    private bool waveOnGoing = false;
    public int currentWave = 0;
    private int waveEnemiesAlive = 0;
    private int enemiesPerWave => currentWave * waveEnemiesCount;
    public void EnemyKilled() => waveEnemiesAlive--;
    public TextMeshProUGUI waveNumber;
    public TextMeshProUGUI enemiesNumber;
    public GameObject pressETip;

    private void Awake()
    {
        Instance = this;
        waveOnGoing = false;
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
        enemiesNumber.text = waveEnemiesAlive.ToString();
    }
    public void FinishWave()
    {
        waveOnGoing = false;
        StartCoroutine(ShowUpgrades());
        pressETip.SetActive(true);
    }
    public void StartWave()
    {
        pressETip.SetActive(false);
        Character.Instance.FullHeal();
        currentWave++;
        waveNumber.text = currentWave.ToString();
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
    IEnumerator ShowUpgrades()
    {
        yield return new WaitForSeconds(0.8f);
        Upgrades.Instance.ShowUpgrades();
    }
}
