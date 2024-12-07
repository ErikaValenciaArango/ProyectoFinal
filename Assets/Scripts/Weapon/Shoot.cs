using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Referencia al punto de disparo
    [SerializeField] private Transform shootPoint;

    // Tiempo de cooldown entre disparos
    [SerializeField] private float shootCooldown = 0.5f;
    private float nextShootTime = 0f;

    private InputManager inputManager;
    
    private AudioClip shootClip;

    int cargador; //van a ser las municiones actuales del arma
    public int cargadorFull = 0; // la municion maxima que va a tener el cargador


    /// <summary>
    /// Identacion de municion y armas
    /// </summary>

    private void Start()
    {
        inputManager = InputManager.Instance;

        cargador = cargadorFull;

        // Carga el sonido desde Resources
        shootClip = Resources.Load<AudioClip>("Audio/AutoGun_3p_02");

        if (shootClip == null)
        {
            Debug.LogError("No se encontró el audio de disparo en Resources/Sounds/ShootSound");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();

        if(Input.GetKeyDown("r"))
        {
            recharge();
        }
    }

    void Shooting()
    {
        //esta parte verifica si el cargador esta lleno si no lo esta lo devuelve
        if(cargador <= 0)
        {
            return;
        }
        // Verificar si el tiempo actual es mayor o igual al tiempo del próximo disparo permitido
        if (Time.time >= nextShootTime)
        {
            if (inputManager.PlayerAttacked())
            {
                AudioManager.Instance.PlaySFX(shootClip, 0.2f);
                // Instanciar el bullet en la posición del punto de disparo
                GameObject bullet = BulletPool.Instance.RequestBullet();
                bullet.transform.position = shootPoint.position;
                bullet.transform.rotation = shootPoint.rotation;
                bullet.SetActive(true);

                // Calcular la dirección hacia el centro de la pantalla
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
                Vector3 targetPoint;

                if (Physics.Raycast(ray, out hit))
                {
                    targetPoint = hit.point;
                }
                else
                {
                    targetPoint = ray.GetPoint(1000); // Un punto lejano en la dirección del ray
                }

                Vector3 direction = (targetPoint - shootPoint.position).normalized;

                // Debug para inspeccionar la dirección
                Debug.DrawRay(shootPoint.position, direction * 5, Color.red, 2f);

                // Establecer la dirección de la bala
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.SetDirection(direction);

                // Actualizar el tiempo del próximo disparo permitido
                nextShootTime = Time.time + shootCooldown;

                //resta uno a la municion
                cargador -= 1;
            }
        }
    }
    void recharge()
    {
        cargador = cargadorFull;
    }
}
