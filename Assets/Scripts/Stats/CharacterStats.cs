using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;

    [SerializeField] protected bool isDead;


    private void Start()
    {
        InitVariables();
    }

    public virtual void CheckHealth()
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

    public virtual void Die()
    {
        isDead = true;
    }

    public void SetHealthTo ( int healthToSetTo)
    {
        health = healthToSetTo;
        CheckHealth();
    }

    public void TakeDamage(int damage)
    {
        int HealthAfterDamage = health - damage;
        SetHealthTo( HealthAfterDamage );
    }
    public void Heal(int heal)
    {
        int HealthAfterHeald = health + heal;
        SetHealthTo( HealthAfterHeald );
    }

    public void InitVariables()
    {
        maxHealth =100;
        SetHealthTo(maxHealth);
        isDead = false;
    }
}
