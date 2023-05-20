using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyGeneratorScript : MonoBehaviour
{
    public GameObject allyPrefab;
    public List<Sprite> allySprites;

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 spawnPosition = new(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);
            GameObject newAlly = Instantiate(allyPrefab, spawnPosition, Quaternion.identity);
            AllyScript allyScript = newAlly.GetComponent<AllyScript>();
            allyScript.GetComponent<SpriteRenderer>().sprite = allySprites[Random.Range(0, allySprites.Count)];
        }
    }
}