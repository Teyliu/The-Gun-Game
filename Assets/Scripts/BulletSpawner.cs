using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletResource;
    public float minRotation;
    public float maxRotation;
    public int numberOfBullets;
    public bool isRandom;
    public bool shootAtPlayer;
    public bool enableSpread;
    public float spreadAngle;

    public float cooldown;
    float timer;
    public float bulletSpeed;
    public Vector2 bulletVelocity;

    private Transform targetTransform;

    float[] rotations;
    void Start()
    {
        if (shootAtPlayer)
        {
            targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        timer = cooldown;
        rotations = new float[numberOfBullets];
        if (!isRandom)
        {
            DistributedRotations();
        }
    }

    void Update()
    {
        if (timer <= 0)
        {
            SpawnBullets();
            timer = cooldown;
        }
        timer -= Time.deltaTime;
    }

    public float[] RandomRotations()
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            rotations[i] = Random.Range(minRotation, maxRotation);
        }
        return rotations;
    }

    public float[] DistributedRotations()
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            var fraction = (float)i / ((float)numberOfBullets - 1);
            var difference = maxRotation - minRotation;
            var fractionOfDifference = fraction * difference;
            rotations[i] = fractionOfDifference + minRotation;
        }
        return rotations;
    }

    public GameObject[] SpawnBullets()
    {
        if (isRandom)
        {
            RandomRotations();
        }

        GameObject[] spawnedBullets = new GameObject[numberOfBullets];
        for (int i = 0; i < numberOfBullets; i++)
        {
            float spread = 0f;
            if (enableSpread)
            {
                spread = Random.Range(-spreadAngle, spreadAngle);
            }

            Vector3 position = transform.position + (Quaternion.Euler(0f, 0f, rotations[i] + spread) * Vector3.right * bulletSpeed * Time.deltaTime);

            spawnedBullets[i] = Instantiate(bulletResource, position, Quaternion.identity);

            var b = spawnedBullets[i].GetComponent<Bullet>();

            if (shootAtPlayer)
            {
                Vector3 direction = (targetTransform.position - position).normalized;
                b.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

                b.velocity = direction * bulletSpeed;
            }
            else
            {
                b.transform.rotation = Quaternion.Euler(0f, 0f, rotations[i] + spread);
                b.velocity = transform.right * bulletSpeed;
            }
        }
        return spawnedBullets;
    }
}