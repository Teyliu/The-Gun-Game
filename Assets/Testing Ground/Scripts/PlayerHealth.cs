using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int maxArmor = 3;
    public int currentArmor;
    public int currentHealth;
    public float invincibilityTime = 1f;
    public bool isInvincible = false;
    public bool isPlayerShot = false;
    private PlayerScript playerScript;
    public ParticleSystemController particleSystemController; // Reference to the ParticleSystemController script
    public bool IsDead { get; private set; }

    private void Start()
    {
        currentHealth = maxHealth;
        playerScript = PlayerScript.Instance;
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            if(currentArmor >= 1)
            {
                currentArmor -= damage;
                StartCoroutine(Invincibility());
            }
            else
            {
                currentHealth -= damage;

                if (currentHealth <= 1)
                {
                    Die();
                }
                else
                {
                    StartCoroutine(Invincibility());
                }
            }
           
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void Armor(int amount)
    {
        currentArmor += amount;
        currentArmor = Mathf.Clamp(currentArmor, 0, maxArmor);
    }

    IEnumerator Invincibility()
    {
        playerScript.GetShot();
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
        playerScript.GetShot();
    }

    public void Die()
    {
        Debug.Log("Player died.");
        IsDead = true;
    }
}