using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugKill : MonoBehaviour
{
    public Collider2D killCollider;
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerMovement player = hitInfo.GetComponent<PlayerMovement>();
        if (player != null && killCollider.IsTouching(hitInfo))
        {
            Debug.Log("hit");
            player.TakeDamage(1000);
        }
    }
}
