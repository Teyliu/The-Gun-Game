using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyGeneratorScript : MonoBehaviour
{
    public List<Sprite> allySprites; 
    public float spawnDelay = 2f;
    public float spawnRange = 5f;
    public int maxAllies = 5;
    public GameObject allyPrefab; 

    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Time.time + spawnDelay;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime && PlayerScript.Instance.alliesCount < maxAllies)
        {
            SpawnAlly();
            nextSpawnTime = Time.time + spawnDelay;
        }
    }

    void SpawnAlly()
    {
        Vector3 spawnPosition = GenerateRandomSpawnPosition();
        GameObject ally = Instantiate(allyPrefab, spawnPosition, Quaternion.identity);
        AllyScript allyScript = ally.GetComponent<AllyScript>();

        if (allyScript != null)
        {
            allyScript.SetTarget(PlayerScript.Instance.transform);

            SpriteRenderer spriteRenderer = ally.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null && allySprites.Count > 0)
            {
                int randomIndex = Random.Range(0, allySprites.Count);
                spriteRenderer.sprite = allySprites[randomIndex];
            }
        }

        PlayerScript.Instance.AddAlly();
    }

        Vector3 GenerateRandomSpawnPosition()
    {
        Vector3 playerPosition = PlayerScript.Instance.transform.position;
        Vector2 randomOffset = Random.insideUnitCircle * spawnRange;
        Vector3 spawnPosition = new(playerPosition.x + randomOffset.x, playerPosition.y + randomOffset.y, playerPosition.z);
        return spawnPosition;
    }
}