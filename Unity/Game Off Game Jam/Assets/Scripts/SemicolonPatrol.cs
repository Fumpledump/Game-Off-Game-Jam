using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemicolonPatrol : MonoBehaviour
{
    [Header("Enemy Settings")]
    public bool mustPatrol = true;
    public float moveSpeed = 2f;
    public float moveDirection = 1;
    public bool facingRight = true;
    public float circleRadius = 1f;
    public Rigidbody2D rb;
    public Animator animator;
    public LayerMask groundLayer;

    private bool grounded = true;
    private bool noObstacle = true;


    // Start is called before the first frame update
    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("mustPatrol", mustPatrol);
        if (mustPatrol)
        {
            Patrol();
        }
    }

    private void FixedUpdate()
    {
    }

    public void Patrol()
    {
        if(!grounded || !noObstacle)
        {
            if(facingRight)
            {
                Flip();
            }else if(!facingRight)
            {
                Flip();
            }
        }
        rb.velocity = new Vector2(moveSpeed * moveDirection, rb.velocity.y);
    }

    private void Flip()
    {
        StartCoroutine(RestStop());
        moveSpeed *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    IEnumerator RestStop()
    {
        animator.SetBool("moving", false);
        yield return new WaitForSeconds(5);
        animator.SetBool("moving", true);

    }
}
