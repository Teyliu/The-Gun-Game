using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public int spawnCounter;
    public float spawnRate;
    public float spawnRadius;

    private float nextSpawnTime;
    private bool canSpawn;

    void Start()
    {
        nextSpawnTime = spawnRate;
        canSpawn = false;
    }

    void Update()
    {
        if (!canSpawn)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, PlayerScript.Instance.transform.position);
            if (distanceToPlayer <= spawnRadius)
            {
                canSpawn = true;
            }
        }

        if (canSpawn && spawnCounter > 0 && Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            spawnCounter--;
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("Enemy prefabs array is empty. Please assign enemy prefabs.");
            return;
        }

        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyPrefab = enemyPrefabs[randomIndex];

        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
