using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;

    [SerializeField] protected bool isDead;

    public void CheckHead()
    {
        if (health <= 0 )
        {
            health = 0;
            Die();
        }
        if (health >= maxHealth )
        {
            health = maxHealth;
        }
    }

    public void Die()
    {
        isDead = true;
    }

    public void SetHealthTo ( int health)
    {

    }
}
