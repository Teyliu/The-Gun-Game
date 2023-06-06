using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float movementSpeed = 0.0025f;
    public int health = 3;
    public int contactDamage = 1;
    public AudioClip[] deathSounds;
    public Drop[] drops; 

    private bool isDead = false;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDead)
        {
            Vector2 direction = (Vector2)PlayerScript.Instance.transform.position - (Vector2)transform.position;
            transform.Translate(movementSpeed * Time.deltaTime * direction.normalized);

            if (direction.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (direction.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            animator.SetFloat("Movement", Mathf.Abs(direction.magnitude));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
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

            BulletDamage bullet = other.GetComponent<BulletDamage>();
            if (bullet != null)
            {
                health -= bullet.damage;
                Destroy(other.gameObject);
                animator.SetTrigger("Die");

                if (health <= 0)
                {
                    isDead = true;
                    StartCoroutine(Die());
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

        yield return new WaitForSeconds(0.0f);

        Destroy(gameObject);
    }
}

[System.Serializable]
public class Drop
{
    public GameObject item;
    public float chance = 0.5f;
}