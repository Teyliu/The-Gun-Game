using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProofVest : MonoBehaviour
{
    public int armorAmount = 3;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Armor(armorAmount);
                Destroy(gameObject);
            }
        }
    }
}
