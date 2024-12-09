using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorElevator : MonoBehaviour
{
    public bool Open = false;
    Animator Animator;
    private void Start() 
    {
        Animator = GetComponentInParent<Animator>();    
    }

    public void abrir()
    {
        Open = true;
        Animator.SetBool("Abrir",Open);
    }
    public void Cerrar()
    {
        Open = false;
        Animator.SetBool("Abrir",Open);
    }

}
