using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    public float bulletDelay = 2f;


    [Header("Components")]
    public Transform firePoint;
    public GameObject bulletPrefab;

    // Holding Fire Variables
    private float timestamp;
    private bool canShoot;

    // Player Input
    private PlayerInputActions playerInput;
    bool holdFire = false;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        playerInput = new PlayerInputActions();
    }

    void OnEnable()
    {
        playerInput.Player.Fire.performed += Shoot;
        playerInput.Player.Fire.Enable();
    }

    // This function is called when the behaviour becomes disabled.
    void OnDisable()
    {
        playerInput.Player.Fire.Disable();
    }

    void Shoot(InputAction.CallbackContext obj)
    {
        holdFire = !holdFire;
    }

    void Update()
    {
        // Shooting
        if (canShoot && holdFire)
        {
            canShoot = false;
            timestamp = Time.time + bulletDelay;
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
        else if (Time.time > timestamp)
        {
            canShoot = true;
        }
    }
}
