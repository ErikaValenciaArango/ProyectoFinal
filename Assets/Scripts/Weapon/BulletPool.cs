using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 30;
    private List<GameObject> bulletList = new List<GameObject>();

    // Patron Singleton
    private static BulletPool instance;
    public static BulletPool Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        AddBulletsToPool(poolSize, bulletPrefab);
    }

    private void AddBulletsToPool(int amount, GameObject bulletPrefab)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bullet.GetComponent<Bullet>().SetPool(this);
            bullet.SetActive(false);
            bulletList.Add(bullet);
        }
    }

    // Funcion que permite instanciar una bala desde otro script 
    public GameObject RequestBullet()
    {
        foreach (var bullet in bulletList)
        {
            if (!bullet.activeSelf)
            {
                return bullet;
            }
        }
        AddBulletsToPool(1, bulletPrefab);
        bulletList[bulletList.Count - 1].SetActive(true);
        return bulletList[bulletList.Count - 1];
    }

    public void ReturnBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
    }
}
