using UnityEngine;
using System.Collections;

public class GunUpgradeScript : MonoBehaviour
{
    public float fireRateModifier = 0.5f;
    public float duration = 10f;
    public AudioClip pickupSound;

    private ShootingScript shootingScript;
    private float baseFireRate;

    void Start()
    {
        shootingScript = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ShootingScript>();
        baseFireRate = shootingScript.baseFireRate;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shootingScript.IncreaseFireRate(fireRateModifier);
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            StartCoroutine(ResetFireRateAfterDelay(duration));
            Destroy(gameObject);
        }
    }

    IEnumerator ResetFireRateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        shootingScript.IncreaseFireRate(-fireRateModifier);
    }
}