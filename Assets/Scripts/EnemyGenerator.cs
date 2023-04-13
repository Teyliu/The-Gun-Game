using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 1.5f;

    IEnumerator Start()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-8f, 8f), 6f);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}