using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public int damage = 1;
    public float lifetime = 2f;
    public AudioClip[] spawnSounds;

    private void Start()
    {
        if (spawnSounds.Length > 0)
        {
            AudioClip clip = spawnSounds[Random.Range(0, spawnSounds.Length)];
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }

        Destroy(gameObject, lifetime);
    }


}