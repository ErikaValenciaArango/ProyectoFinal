using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStats : CharacterStats
{


    private void Start()
    {
        InitVariables();
    }

    public void DealDamage()
    {

    }

    public override void Die()
    {
        base.Die();
    }

    public override void InitVariables()
    {
        maxHealth = 120;
        SetHealthTo(maxHealth);
        isDead = false;
    }
}
