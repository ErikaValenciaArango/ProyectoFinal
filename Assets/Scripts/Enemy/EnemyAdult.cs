using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAdult : Enemy
{
    private NavMeshAgent agent;
    public Animator animationChild;
    private Collider enemyCollider;
    public EnemyStats statsEnemy;



    // Variables para el patrullaje del enemigo
    public Transform[] checkPoints;
    private int indice = 0;
    //Revision Optimisacion de distance 
    public float distanceCheckpoints;
    private float distanceCheckpoints2;


    // Tiempo de espera en los puntos de patrullaje
    public float waitTime = 7f;
    private bool isWaiting = false;

    //Dano del enemigo
    public float damage = 10f;


    void Start()
    {
        //Esto me permite ejecutar el Awake del elemento padre ya que si no me lo sobre escribe y no ejecuta este Awake
        agent = GetComponent<NavMeshAgent>();
        enemyCollider = GetComponent<Collider>();
        statsEnemy = GetComponent<EnemyStats>();

        agent.speed = 2f; // Ajusta este valor según la velocidad deseada



        //Se leva al cuadrado la distancia de los checkpoints
        distanceCheckpoints2 = distanceCheckpoints * distanceCheckpoints;
    }

    public override void EstadoIdle()
    {
        base.EstadoIdle();
        if (statsEnemy.isDead == true)
        {
            Death();
        }
        if (isWaiting) return; // Si está esperando, no hacer nada más

        if (animationChild != null) animationChild.SetFloat("speed", 1f);
        if (animationChild != null) animationChild.SetBool("attack", false);
        agent.SetDestination(checkPoints[indice].position);

        //Version optimizada de movimiento a checkpoints
        if ((checkPoints[indice].position - transform.position).sqrMagnitude < distanceCheckpoints2)
        {
            StartCoroutine(WaitAtCheckpoint());
        }
    }

    private IEnumerator WaitAtCheckpoint()
    {
        isWaiting = true;
        animationChild.SetFloat("speed", 0f); // Animación de pausa
        yield return new WaitForSeconds(waitTime);
        indice = (indice + 1) % checkPoints.Length; // Ir al siguiente punto
        isWaiting = false;
    }
    public override void EstadoFollow()
    {
        base.EstadoFollow();
        if (animationChild != null) animationChild.SetFloat("speed", 1f);
        if (animationChild != null) animationChild.SetBool("attack", false);
        //transform.LookAt(target, Vector3.up);
        if(agent != null && target != null)
        {
            agent.SetDestination(target.position);
            transform.LookAt(target, Vector3.up);
        }
        if (statsEnemy.isDead == true)
        {
            Death();
        }
    }

    public override void EstadoAttack()
    {
        base.EstadoAttack();
        if (animationChild != null) animationChild.SetFloat("speed", 0f);
        if (animationChild != null) animationChild.SetBool("attack", true);
        //transform.LookAt(target, Vector3.up);
        if (agent != null && target != null)
        {
           agent.SetDestination(target.position);
           transform.LookAt(target, Vector3.up);
        }
        if (statsEnemy.isDead == true)
        {
            Death();
        }

    }

    public override void EstadoDead()
    {
        base.EstadoDead();
        if (animationChild != null) animationChild.SetBool("life", false);
        agent.enabled = false;
        // Desactiva el collider
        if (enemyCollider != null)
        {
            enemyCollider.enabled = false;
        }
    }

    [ContextMenu("Death")]
    public void Death()
    {
        CambiarEstado(Estados.dead);
    }
    public void Attack()
    {
        if (target != null && target != null)
        {
            target.GetComponent<CharacterStats>().TakeDamage((int)damage);
        }
    }
}
