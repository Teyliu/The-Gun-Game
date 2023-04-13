using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float movementSpeed = 0.0025f;
    public int health = 3;

    void Update()
    {
        Vector2 direction = (Vector2)PlayerScript.instance.transform.position - (Vector2)transform.position;
        transform.Translate(direction.normalized * movementSpeed * Time.deltaTime);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        BulletDamage bullet = other.GetComponent<BulletDamage>();
        if (bullet != null)
        {
            health -= bullet.damage; // decrease the health of the enemy by the damage of the bullet
            Destroy(other.gameObject); // destroy the bullet
            if (health <= 0)
            {
                Destroy(gameObject); // destroy the enemy if its health reaches 0
            }
        }
    }
}