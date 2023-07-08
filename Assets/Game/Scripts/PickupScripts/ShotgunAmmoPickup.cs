using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAmmoPickup : MonoBehaviour
{
    public int ammoAmount = 10; // Amount of shotgun ammo to add

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShootingScript shootingScript = other.GetComponent<ShootingScript>();
            if (shootingScript != null)
            {
                shootingScript.AddShotgunAmmo(ammoAmount);
                Destroy(gameObject);
            }
        }
    }
}
