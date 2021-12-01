using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemicolonPatrol : MonoBehaviour
{
    [Header("Enemy Settings")]
    public List<Transform> points;
    public Transform enemy;
    public bool mustPatrol = true;
    public float walkSpeed = 2f;
    public Rigidbody2D rb;
    public Animator animator;
    // Enemy
    int goalPoint = 0;

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
            MoveToNextPoint();
        }
    }

    public void Patrol()
    {
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    private void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
    }

    private void MoveToNextPoint()
    {
        // Change the position of the platform (move towards the goal point)
        enemy.position = Vector2.MoveTowards(enemy.position, points[goalPoint].position, 1 * Time.deltaTime * walkSpeed);

        // Check if we are in very close proximity of the next point
        if (Vector2.Distance(enemy.position, points[goalPoint].position) < 0.1f)
        {
            if (goalPoint == points.Count - 1)
            {
                goalPoint = 0;
            }
            else
            {
                goalPoint++;
            }
        }
    }
}
