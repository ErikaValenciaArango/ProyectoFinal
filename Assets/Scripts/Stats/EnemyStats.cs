using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private ZombieSound zombieSound;

    private void Start()
    {
        InitVariables();
        zombieSound = GetComponent<ZombieSound>();
    }

    public void DealDamage()
    {

    }

    public override void Die()
    {
        base.Die();

        if (zombieSound != null)
        {
            zombieSound.OnEnemyDeath();
        }
    }

    public override void InitVariables()
    {
        maxHealth = 120;
        SetHealthTo(maxHealth);
        isDead = false;
    }
}
