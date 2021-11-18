using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    public float runSpeed = 40f;

    [Header("Components")]
    public CharacterController2D controller;
    public Animator animator;

    // Player Input
    private PlayerInputActions playerInput;
    private InputAction movement;

    // Update Transfer Variables
    float horizontalMove = 0f;
    bool jump = false;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        // If Component is Empty it automatically grabs it.
        if(controller == null) controller = GetComponent<CharacterController2D>();
        if(animator == null) animator = GetComponent<Animator>();
        playerInput = new PlayerInputActions();
    }
    // This function is called when the object becomes enabled and active.
    void OnEnable()
    {
        movement = playerInput.Player.Movement;
        movement.Enable();

        playerInput.Player.Jump.performed += DoJump;
        playerInput.Player.Jump.Enable();
    }

    void DoJump(InputAction.CallbackContext obj)
    {
        jump = true;
        animator.SetBool("Jump", true);
    }

    public void OnLanding()
    {
        animator.SetBool("Jump", false);
    }

    // This function is called when the behaviour becomes disabled.
    void OnDisable()
    {
        movement.Disable();
        playerInput.Player.Jump.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Running
        horizontalMove = movement.ReadValue<Vector2>().x * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        // Jumping
        if (controller.m_Grounded == false)
        {
            animator.SetBool("Jump", !controller.m_Grounded);
        }
        else
        {
            animator.SetBool("Jump", !controller.m_Grounded);
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
