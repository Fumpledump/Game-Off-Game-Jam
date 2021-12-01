using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    public float runSpeed = 40f;
    public int health = 10;

    [Header("Components")]
    public CharacterController2D controller;
    public Animator animator;
    public AnimationClip deathAnimation;
    public Rigidbody2D rb;

    // Player Input
    private PlayerInputActions playerInput;
    private InputAction movement;
    private bool dead = false;
    private bool glide = false;

    // Update Transfer Variables
    float horizontalMove = 0f;
    bool jump = false;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        // If Component is Empty it automatically grabs it.
        if(controller == null) controller = GetComponent<CharacterController2D>();
        if(animator == null) animator = GetComponent<Animator>();
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        playerInput = new PlayerInputActions();
    }
    // This function is called when the object becomes enabled and active.
    void OnEnable()
    {
        movement = playerInput.Player.Movement;
        movement.Enable();

        playerInput.Player.Jump.performed += DoJump;
        playerInput.Player.Jump.Enable();

        playerInput.Player.Glide.performed += DoGlide;
        playerInput.Player.Glide.Enable();
    }

    void DoJump(InputAction.CallbackContext obj)
    {
        jump = true;
        animator.SetBool("Jump", true);
    }

    void DoGlide(InputAction.CallbackContext obj)
    {
        if (!controller.m_Grounded && !glide)
        {
            glide = true;
            animator.SetBool("Glide", true);
            rb.velocity = transform.position / 2;
        }
    }

    public void OnLanding()
    {
        animator.SetBool("Jump", false);
        animator.SetBool("Glide", false);
    }

    // This function is called when the behaviour becomes disabled.
    void OnDisable()
    {
        movement.Disable();
        playerInput.Player.Jump.Disable();
        playerInput.Player.Glide.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            return;
        }
        // Running
        horizontalMove = movement.ReadValue<Vector2>().x * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        // Jumping
        if (controller.m_Grounded == false)
        {
            float oldSpeed = runSpeed;
            float oldScale = rb.gravityScale;
            if (glide)
            {
                rb.gravityScale = 0.2f;
                runSpeed = 35;
            }
            else
            {
                rb.gravityScale = 3;
                runSpeed = 40;
            }
            animator.SetBool("Jump", !controller.m_Grounded);
        }
        else
        {
            glide = false;
            animator.SetBool("Jump", !controller.m_Grounded);
            animator.SetBool("Glide", !controller.m_Grounded);
        }
    }

    void FixedUpdate()
    {
        if (dead)
        {
            controller.Move(0 * Time.fixedDeltaTime, false, jump);
            return;
        }
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        animator.SetInteger("Health", health);

        if (health <= 0)
        {
            animator.SetInteger("Health", 0);
            dead = true;
        }
    }
}
