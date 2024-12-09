using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 6f;
    [SerializeField] private Rigidbody bulletRb;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private float damage = 10f; // Cantidad de daño que la bala inflige

    private void OnTriggerEnter(Collider other)
    {
        // Aplicar daño si el objeto impactado tiene un componente Health
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
        ReturnToPool();
    }

    public void SetPool(BulletPool bulletPool)
    {
        this.bulletPool = bulletPool;
    }

    public void SetDirection(Vector3 direction)
    {
        bulletRb.velocity = direction * bulletSpeed;
    }

    private void ReturnToPool()
    {
        // Si no tenemos referencia al pool, intentar obtenerla del singleton
        if (bulletPool == null)
        {
            bulletPool = BulletPool.Instance;
            if (bulletPool == null)
            {
                Debug.LogError("BulletPool instance not found in scene.");
                return;
            }
        }
        bulletPool.ReturnBulletToPool(gameObject);
    }
}
