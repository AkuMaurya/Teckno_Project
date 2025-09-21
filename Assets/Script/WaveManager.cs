using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;    
    public GameObject player;          
    public int enemiesPerWave = 100;  
    public float spawnRadius = 20f;   
    public float spawnDelay = 0.1f;
    private int currentWave = 0;
    public Transform enemiesParent;
    public int aliveEnemies = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void StartWave()
    {
        player = GameObject.Find("Player(Clone)");
        currentWave++;
        StartCoroutine(SpawnWave());
    }
    public void DeleteAllChildren()
    {
        foreach (Transform child in enemiesParent)
        {
            Destroy(child.gameObject);
        }
        aliveEnemies = 0;
    }
    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < enemiesPerWave; i++)
        {
            Vector3 spawnPos = player.transform.position + (Random.insideUnitSphere * spawnRadius);
            spawnPos.y = 0f; // keep on ground
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            enemy.transform.SetParent(enemiesParent);
            aliveEnemies++;
            yield return new WaitForSeconds(spawnDelay);
        }
    }
    public void EnemyDied()
    {
        aliveEnemies--;

        if (aliveEnemies <= 0) // all enemies dead
        {
            Debug.Log("Wave " + currentWave + " cleared!");
            StartWave(); // start next wave
        }
    }

}
