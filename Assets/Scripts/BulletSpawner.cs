using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletResource;
    public int numberOfBullets;
    public bool enableSpread;
    public float spreadAngle;
    public float minSpeed;
    public float maxSpeed;

    public float bulletSpeed;

    private Transform targetTransform;

    float[] rotations;
    float spawnCooldown;

    void Start()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rotations = new float[numberOfBullets];
        spawnCooldown = 0f;
    }

    public float[] DistributedRotations()
    {
        float angleStep = 50f / numberOfBullets;
        float currentAngle = 0f;
        for (int i = 0; i < numberOfBullets; i++)
        {
            rotations[i] = currentAngle;
            currentAngle += angleStep;
        }
        return rotations;
    }

    void Update()
    {
        spawnCooldown -= Time.deltaTime;

        if (spawnCooldown <= 0f)
        {
            SpawnBullets();
            spawnCooldown = bulletSpeed;
        }
    }

    public GameObject[] SpawnBullets()
    {
        DistributedRotations();

        GameObject[] spawnedBullets = new GameObject[numberOfBullets];
        for (int i = 0; i < numberOfBullets; i++)
        {
            float spread = 0f;
            if (enableSpread)
            {
                spread = Random.Range(-spreadAngle, spreadAngle);
            }

            Vector3 direction = targetTransform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle += rotations[i] + spread;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            float speed = Random.Range(minSpeed, maxSpeed);

            Vector3 bulletPosition = transform.position + rotation * Vector3.right * speed * Time.deltaTime;
            Vector3 offset = Random.insideUnitCircle * 0.1f;

            spawnedBullets[i] = Instantiate(bulletResource, bulletPosition + offset, rotation);

            var b = spawnedBullets[i].GetComponent<Bullet>();
            b.SetDirection(rotation * Vector3.right);
            b.speed = speed;
        }

        return spawnedBullets;
    }
}