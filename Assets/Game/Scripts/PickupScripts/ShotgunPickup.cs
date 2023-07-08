using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeWeapon changeWeapon = other.GetComponent<ChangeWeapon>();
            if (changeWeapon != null)
            {
                changeWeapon.isShotgunAvailable = true;
                Destroy(gameObject);
            }
        }
    }
}
