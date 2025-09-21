using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    public Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Ideal()
    {
        animator.SetBool("Attack", false);
    }

    public void Attack()
    {
        animator.SetBool("Attack", true);
    }
}
