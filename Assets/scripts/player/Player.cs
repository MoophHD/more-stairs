using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	const float speed = 5;
	private Rigidbody rb;
	
	void Awake() {
		rb = GetComponent<Rigidbody>();
	}

	public void setVelocity( bool isRight ) {
		Vector3 dir = isRight ? Vector3.right : Vector3.forward;

		rb.velocity = dir * speed;
	}
}
