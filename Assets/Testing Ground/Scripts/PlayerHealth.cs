using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float invincibilityTime = 1f;
    public bool isInvincible = false;
    public bool isPlayerShot = false;
    private PlayerScript playerScript;
    public EventHandler Died;
    public ParticleSystemController particleSystemController; // Reference to the ParticleSystemController script

    private void Start()
    {
        currentHealth = maxHealth;
        playerScript = PlayerScript.Instance;
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(Invincibility());
            }
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    IEnumerator Invincibility()
    {
        playerScript.GetShot();
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
        playerScript.GetShot();
    }

    void Die()
    {
        Debug.Log("Player died.");
    }
}