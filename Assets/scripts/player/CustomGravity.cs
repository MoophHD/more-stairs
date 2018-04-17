using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomGravity : MonoBehaviour
{
    public float gravityScale = 0;

    public static float globalGravity = -10f;

    Rigidbody rb;

    private bool isOnGround = true;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        if (!isOnGround) {
            Vector3 gravity = globalGravity * gravityScale * Vector3.up;
            rb.AddForce(gravity, ForceMode.Acceleration);
        } else {
            resetVelocityY();
        }

        isOnGround = false;
    }

    void resetVelocityY() {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    }

    void OnCollisionEnter(Collision other) {
        if (other.transform.tag == "block")
            resetVelocityY();
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "block")
            isOnGround = true;

    }
    void OnTriggerStay(Collider other) {
        if (other.tag == "block")
            isOnGround = true;
    }
}