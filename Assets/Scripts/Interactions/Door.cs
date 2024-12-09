using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
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
        Animator.SetBool("Abrir",Open);
    }
}
