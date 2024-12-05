using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Lógica para manejar la muerte del enemigo
        Enemy enemy = GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.CambiarEstado(Enemy.Estados.dead);
        }
        // Desactivar el objeto o cualquier otra lógica de muerte
        gameObject.SetActive(false);
    }
}
