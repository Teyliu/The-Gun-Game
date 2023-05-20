using UnityEngine;
using System.Collections;

public class GunUpgradeScript : MonoBehaviour
{
    public float fireRateModifier = 0.5f;
    public float duration = 10f;
    public AudioClip pickupSound;

    private ShootingScript shootingScript;

    private void Start()
    {
        shootingScript = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ShootingScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shootingScript.IncreaseFireRate(fireRateModifier, duration);
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            Destroy(gameObject);
        }
    }
}