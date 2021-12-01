using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 20f;
    public int damage = 50;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public AnimationClip bulletHit;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Player")
        {
            return;
        }

        rb.velocity = transform.position;

        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        StartCoroutine(DestroyBullet());
        DestroyBullet();
    }
    IEnumerator DestroyBullet()
    {
        animator.SetBool("Hit", true);
        yield return new WaitForSeconds(bulletHit.length);
        Destroy(gameObject);
    }
}
