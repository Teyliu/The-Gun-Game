using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public int damage = 1;
    public float lifetime = 2f;
    public AudioClip[] spawnSounds;
    public string[] ignoredTags;

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
        foreach (Collider2D collider in colliders)
        {
            foreach (string ignoredTag in ignoredTags)
            {
                if (collision.gameObject.CompareTag(ignoredTag))
                {
                    Physics2D.IgnoreCollision(collider, collision.collider);
                    break;
                }
            }
        }

        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}

