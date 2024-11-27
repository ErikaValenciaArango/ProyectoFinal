using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class Enemy : MonoBehaviour
{
    public Estados estado;

    //Distancias
    public float distanciaSeguir;
    public float distanciaAtacar;
    public float distanciaEscapar;
    public float distancia;// Distancia del jugador al enemigo

    //Posiciones
    public Transform target;

    //Eliminar cuando se quiten los pivotes
    public Transform pivot;
    /////

    //Verificadores
    public bool autoSeleccionarTarget = true;// seleccinar cual va ha ser el objetivo
    public bool vivo = true;

    private void Awake()
    {
        if (autoSeleccionarTarget)
        {
                    target = GameObject.FindGameObjectWithTag("Player").transform;
                    //Eliminar cuando se cambie el pivote
                    pivot = target.Find("Pivot").transform;

            /////
        }
        StartCoroutine(CalcularDistancia());//Inicializa la corutina
    }

    //Verifica el estado despues del Update
    private void LateUpdate()
    {
        CheckEstado();
    }

    public enum Estados
    {
        idle = 0,
        follow = 1,
        attack = 2,
        dead = 3,
    }

    /// <summary>
    /// Se establecieron los Gizmos para tener la visualisacion del cambio de estados y a si mismo los rangos donde van a interactuar los
    /// activadores de los estados.
    /// </summary>

    void CheckEstado()
    {
        switch (estado)
        {
            case Estados.idle:
                EstadoIdle();
                break;
            case Estados.follow:
                EstadoFollow();
                break;
            case Estados.attack:
                EstadoAttack();
                break;
            case Estados.dead:
                EstadoDead();
                break;
            default:
                break;
        }
    }
    public void CambiarEstado(Estados e)
    {
        switch (e)
        {
            case Estados.idle:
                break;
            case Estados.follow:
                break;
            case Estados.attack:
                break;
            case Estados.dead:
                vivo = false;
                break;
            default:
                break;
        }
        estado = e;
    }

    public virtual void EstadoIdle()
    {
        if(distancia < distanciaSeguir)
        {
            CambiarEstado (Estados.follow);
        }
    }
    public virtual void EstadoFollow()
    {
        if (distancia < distanciaAtacar)
        {
            CambiarEstado(Estados.attack);
        }
        if (distancia > distanciaEscapar)
        {
            CambiarEstado(Estados.idle);
        }
    }
    public virtual void EstadoAttack()
    {
        if(distancia > distanciaAtacar + 0.4f) //Se le suma la distancia en la que va a continuar siguiendolo
        {
            CambiarEstado(Estados.follow);
        }
    }
    public virtual void EstadoDead()
    {

    }
    /// <summary>
    /// Se calculan las distancias para interactuar con el jugador
    /// </summary>
    IEnumerator CalcularDistancia()
    {
        while (vivo)
        {
            yield return new WaitForSeconds(0.2f);// cada tercio de segundo verifica la posicion
            if(target != null)
            {
                distancia = Vector3.Distance(transform.position,target.position);//calcula la distancia entre el objeto Player y el enemigo

            }
        }
    }
    /// <summary>
    /// Se establecieron los Gizmos para tener la visualisacion del cambio de estados y a si mismo los rangos donde van a interactuar los
    /// activadores de los estados.
    /// </summary>

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position,Vector3.up,distanciaAtacar);
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, Vector3.up, distanciaSeguir);
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, Vector3.up, distanciaEscapar);
    }

#endif
    private void OnDrawGizmos()
    {
        int icono = (int)estado;

        switch (icono)
        {
            case 0:
                Gizmos.DrawIcon(transform.position + Vector3.up * 2f, "idle.png");
                break;
            case 1:
                Gizmos.DrawIcon(transform.position + Vector3.up * 2f, "follow.png");
                break;
            case 2:
                Gizmos.DrawIcon(transform.position + Vector3.up * 2f, "atacar.png");
                break;
            case 3:
                Gizmos.DrawIcon(transform.position + Vector3.up * 2f, "morir.png");
                break;
            default:
                UnityEngine.Debug.Log("Estado no encontrado");
                break;
        }
    }


}
