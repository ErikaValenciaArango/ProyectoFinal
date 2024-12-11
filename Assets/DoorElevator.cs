using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorElevator : MonoBehaviour
{
    public bool Open = false;
    public bool HasFuse = false; // Variable para verificar si el fusible ha sido recogida
    public Animator Animator;
    private void Start()
    {
        Animator = GetComponentInParent<Animator>();
    }

    public void abrir()
    {
        if (HasFuse)
        {
            Open = true;
            Animator.SetBool("Abrir", Open);
        }

    }
    public void Cerrar()
    {
        Open = false;
        Animator.SetBool("Abrir", Open);
    }

}
