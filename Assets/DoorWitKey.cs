using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWitKey : MonoBehaviour
{
    public bool Open = false;
    Animator Animator;
    private void Start() 
    {
        Animator = GetComponentInParent<Animator>();    
    }
    public void CambiarValor()
    {
        Open = !Open;
        Animator.SetTrigger("Interactuo?");
        Animator.SetBool("Abrir",Open);
    }
}
