using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyChild : Enemy
{
    private NavMeshAgent agent;
    public Animator animationChild;

    //Dano del enemigo
    public float damage = 10f;


    void Start()
    {
        //Esto me permite ejecutar el Awake del elemento padre ya que si no me lo sobre escribe y no ejecuta este Awake
        agent = GetComponent<NavMeshAgent>();
    }

    public override void EstadoIdle()
    {
        base.EstadoIdle();
        if (animationChild != null) animationChild.SetFloat("speed", 0);
        if (animationChild != null) animationChild.SetBool("attack", false);
        agent.SetDestination(transform.position);
    }
    public override void EstadoFollow()
    {
        base.EstadoFollow();
        if (animationChild != null) animationChild.SetFloat("speed", 1f);
        if (animationChild != null) animationChild.SetBool("attack", false);
        if (agent != null && target != null)
        {
            agent.SetDestination(target.position);
            transform.LookAt(target, Vector3.up);
        }

    }

    public override void EstadoAttack()
    {
        base.EstadoAttack();
        if (animationChild != null) animationChild.SetFloat("speed",0f);
        if (animationChild != null) animationChild.SetBool("attack",true);
        if (agent != null && target != null)
        {
            //agent.SetDestination(target.position);
            transform.LookAt(target, Vector3.up);
        }
    }

    public override void EstadoDead()
    {
        base.EstadoDead();
        if (animationChild != null) animationChild.SetBool("life",false);
        agent.enabled = false;
    }

    [ContextMenu("Death")]
    public void Death()
    {
        CambiarEstado(Estados.dead);
    }
    public void Attack()
    {
        if (agent != null && target != null)
        {
            target.GetComponent<CharacterStats>().TakeDamage((int)damage);
        }
    }

}
