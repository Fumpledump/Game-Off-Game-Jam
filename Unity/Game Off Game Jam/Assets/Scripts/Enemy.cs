using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public int health = 3;
    [Header("Components")]
    public Animator animator;

    public void TakeDamage (int damage)
    {
        health -= damage;
        animator.SetInteger("Health", health);

        if (health <= 0)
        {
            animator.SetInteger("Health", 0);
            Destroy(gameObject);
        }
    }
}
