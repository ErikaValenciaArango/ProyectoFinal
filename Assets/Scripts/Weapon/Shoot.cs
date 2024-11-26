using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shoot : MonoBehaviour
{
    // Referencia al punto de disparo
    [SerializeField] private Transform shootPoint;

    // Tiempo de cooldown entre disparos
    [SerializeField] private float shootCooldown = 0.5f;
    private float nextShootTime = 0f;

    // Update is called once per frame
    void Update()
    {
        Shooting();
    }

    void Shooting()
    {
        // Verificar si el tiempo actual es mayor o igual al tiempo del próximo disparo permitido
        if (Time.time >= nextShootTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                // Instanciar el bullet en la posición del punto de disparo
                GameObject bullet = BulletPool.Instance.RequestBullet();
                bullet.transform.position = shootPoint.position;
                bullet.transform.rotation = shootPoint.rotation;
                bullet.SetActive(true);

                // Establecer la dirección de la bala
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.SetDirection(shootPoint.forward);

                // Actualizar el tiempo del próximo disparo permitido
                nextShootTime = Time.time + shootCooldown;
            }
        }
    }
}
