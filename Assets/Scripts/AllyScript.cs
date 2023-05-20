using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyScript : MonoBehaviour
{
    public float movementSpeed = 0.0025f;
    public int health = 3;
    public int damage = 1;

    private EnemyScript target;
    private bool isDead = false;

    void Update()
    {
        if (!isDead && target != null)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance > 2.0f) 
            {
                Vector2 direction = (Vector2)target.transform.position - (Vector2)transform.position;
                transform.Translate(movementSpeed * Time.deltaTime * direction.normalized);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDead)
        {
            if (other.CompareTag("Enemy"))
            {
                EnemyScript enemyScript = other.GetComponent<EnemyScript>();
                if (enemyScript != null)
                {
                    target = enemyScript;
                }
            }

            BulletDamage bullet = other.GetComponent<BulletDamage>();
            if (bullet != null)
            {
                health -= bullet.damage;
                Destroy(other.gameObject);

                if (health <= 0)
                {
                    isDead = true;
                    Destroy(gameObject);
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!isDead && target != null && other.gameObject == target.gameObject)
        {
            target.health -= damage;
            if (target.health <= 0)
            {
                isDead = true;
                Destroy(target.gameObject);
            }
        }
    }
}