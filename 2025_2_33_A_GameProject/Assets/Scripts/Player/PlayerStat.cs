using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public int currentHealth = 0;
    public int maxHealth = 10;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
            Debug.Log("풀피");
        }
        Debug.Log("현재체력 :" + currentHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth += amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("사망");
        }
        Debug.Log("현재체력 :" + currentHealth);
    }
}
