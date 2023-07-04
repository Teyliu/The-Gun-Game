using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;
    public float lifetime;
    public AudioClip[] bulletSounds;

    private Vector3 direction;

    private void Start()
    {
        if (bulletSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, bulletSounds.Length);
            AudioClip bulletSound = bulletSounds[randomIndex];
            AudioSource.PlayClipAtPoint(bulletSound, transform.position);
        }

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += speed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }



    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }
}