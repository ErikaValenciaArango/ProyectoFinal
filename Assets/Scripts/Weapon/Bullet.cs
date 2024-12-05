using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 6f;
    [SerializeField] private Rigidbody bulletRb;

    [SerializeField] private BulletPool bulletPool;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
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
