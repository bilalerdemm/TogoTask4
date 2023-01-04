using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToJump : MonoBehaviour
{
    public Rigidbody playerRb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerRb.velocity += new Vector3(0, 8, 0);
        }
    }
}
