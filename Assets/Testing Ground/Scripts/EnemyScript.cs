using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float movementSpeed = 2f;
    public int health = 3;
    public int contactDamage = 1;
    public float detectionRange = 5f;
    public AudioClip[] deathSounds;
    public Drop[] drops;

    private bool isDead = false;
    private Animator animator;
    private Transform playerTransform;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerTransform = PlayerScript.Instance.transform;
    }
    private void OnDrawGizmosSelected()
{
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, detectionRange);
}

    void Update()
    {
        if (!isDead)
        {
            Vector2 direction = playerTransform.position - transform.position;
            float distance = direction.magnitude;

            // Check if the player is within the detection range
            if (distance <= detectionRange)
            {
                // Move towards the player smoothly
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, movementSpeed * Time.deltaTime);
            }

            // Flip the sprite based on the movement direction
            if (direction.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (direction.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            animator.SetFloat("Movement", distance);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!isDead)
        {
            if (other.CompareTag("Player"))
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(contactDamage);
                }
            }

            if (!other.isTrigger)
            {
                BulletDamage bulletDamage = other.GetComponent<BulletDamage>();
                if (bulletDamage != null)
                {
                    health -= bulletDamage.damage;
                    Destroy(bulletDamage.gameObject);

                    if (health <= 0)
                    {
                        isDead = true;
                        StartCoroutine(Die());
                        animator.SetTrigger("Die");
                    }
                }
            }
        }
    }


    IEnumerator Die()
    {
        if (deathSounds.Length > 0)
        {
            AudioClip clip = deathSounds[Random.Range(0, deathSounds.Length)];
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }

        foreach (Drop drop in drops)
        {
            if (Random.value < drop.chance)
            {
                Instantiate(drop.item, transform.position, Quaternion.identity);
            }
        }

        yield return new WaitForSeconds(0.6f);

        Destroy(gameObject);
    }
}

[System.Serializable]
public class Drop
{
    public GameObject item;
    public float chance = 0.5f;
}
