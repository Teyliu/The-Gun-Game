using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public int damage = 1;
    public float lifetime = 2f;
    public AudioClip[] spawnSounds;

    public Collider2D[] colliders;

    private void Start()
    {
        if (spawnSounds.Length > 0)
        {
            AudioClip clip = spawnSounds[Random.Range(0, spawnSounds.Length)];
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }

        Destroy(gameObject, lifetime);

        colliders = GetComponents<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (Collider2D collider in colliders)
            {
                Physics2D.IgnoreCollision(collider, collision.collider);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}