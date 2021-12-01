using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public int health = 3;
    public bool getsDestroyed = true;
    [Header("Components")]
    public Animator animator;
    public AnimationClip death;

    public void TakeDamage (int damage)
    {
        health -= damage;
        animator.SetInteger("Health", health);

        if (health <= 0)
        {
            animator.SetInteger("Health", 0);
            if(death == null)
            {
                if (getsDestroyed)
                {
                    Destroy(gameObject);
                }
                else
                {
                    StartCoroutine(Win());
                }
            }
            else
            {
                StartCoroutine(Death());
            }

        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(death.length);
        Destroy(gameObject);
    }
    IEnumerator Win()
    {
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
