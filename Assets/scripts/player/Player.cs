using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	const float speed = 10;
	const float jumpVelocity = 5;
	private Rigidbody rb;

	private bool jumpRequest;
	
	void Awake() {
		rb = GetComponent<Rigidbody>();
	}

	public void setVelocity() {
		Vector3 dir = Vector3.right;

		rb.velocity = dir * speed;
	}

	void Update() {
		// print(rb.velocity);
        if (Input.GetMouseButtonDown(0))
        {
            jumpRequest = true;
        }
	}

	void FixedUpdate() {
        if (jumpRequest) {
			rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            jumpRequest = false;
		}
	}

    // void OnTriggerEnter(Collider other)
    // {
	// 	if (other.tag == "block") {
	// 		if (rb.velocity.y == 0) return;
	// 		//remove vertical velocity
	// 		rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
	// 	}
    // }
}
