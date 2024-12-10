
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : CharacterStats
{
    private PlayerHUD playerHUD;
    [SerializeField]private GameObject gameOver;
    public UnityEvent eventoMorir;

    //Aditional Method
    PostProsessing EffectCamera;


    private void Start()
    {
        GetReferences();
        InitVariables();
    }

    private void GetReferences()
    {
        playerHUD = GetComponent<PlayerHUD>();
        EffectCamera = GetComponentInChildren<PostProsessing>();
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
        gameOver.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    public override void Heal(int heal)
    {
        base.Heal(heal);
        EffectCamera.HealthProsessing();

    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        EffectCamera.DamageProsessing(health);

    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
