using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : CharacterStats
{
    private PlayerHUD playerHUD;
    public UnityEvent eventoMorir;


    private void Start()
    {
        GetReferences();
        InitVariables();
    }

    private void GetReferences()
    {
        playerHUD = GetComponent<PlayerHUD>();
    }

    public override void CheckHealth()
    {
        base.CheckHealth();
        playerHUD.UpdateHealth(health, maxHealth);
    }

    public override void Die()
    {
        base.Die();
        eventoMorir.Invoke();
    }
}
