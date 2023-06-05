using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 1.5f;
    public float followDistance = 5f;
    public Transform playerTransform;

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
        Vector2 spawnPosition = (Vector2)playerTransform.position + Random.insideUnitCircle.normalized * followDistance;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}