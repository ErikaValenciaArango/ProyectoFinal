using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAdult : Enemy
{
    private NavMeshAgent agent;
    public Animator animationChild;

    // Variables para el patrullaje del enemigo
    public Transform[] checkPoints;
    private int indice = 0;
    //Revision Optimisacion de distance 
    public float distanceCheckpoints;
    private float distanceCheckpoints2;


    //Dano del enemigo
    public float damage = 3f;


    void Start()
    {
        //Esto me permite ejecutar el Awake del elemento padre ya que si no me lo sobre escribe y no ejecuta este Awake
        agent = GetComponent<NavMeshAgent>();

        //Se leva al cuadrado la distancia de los checkpoints
        distanceCheckpoints2 = distanceCheckpoints * distanceCheckpoints;
    }

    public override void EstadoIdle()
    {
        base.EstadoIdle();
        if (animationChild != null) animationChild.SetFloat("speed", 1f);
        if (animationChild != null) animationChild.SetBool("attack", false);
        agent.SetDestination(checkPoints[indice].position);

        //Version optimizada de movimiento a checkpoints
        if ((checkPoints[indice].position - transform.position).sqrMagnitude < distanceCheckpoints2)
        {
            //Se genera el indice por medio del residuo de las divisiones de cada posicion sobre el total de puntos en el mapa
            indice = (indice + 1) % checkPoints.Length;
        }
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
            
        }

    public override void EstadoDead()
    {
        base.EstadoDead();
        if (animationChild != null) animationChild.SetBool("life", false);
        agent.enabled = false;
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
            target.GetComponent<Personaje>().personajeVida.CausarDano(damage);
        }
    }
}
