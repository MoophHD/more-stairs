using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	const float speed = 4;
	const float jumpVelocity = 5;

    private Vector3 _currentVelocity;

	private bool jumpRequest;
	public Vector3 startPos;
    private Rigidbody rb;
    private Transform tr;


    public void reset(){
		tr.position = startPos;
		rb.velocity = Vector3.zero;
	}

	void Awake() {
		rb = GetComponent<Rigidbody>();
		tr = GetComponent<Transform>();
        startPos = tr.position;
	}


	public void setVelocity() {
		Vector3 dir = Vector3.right;

		rb.velocity = dir * speed;
	}

	void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            jumpRequest = true;
        }
	}

	private bool collidingWithBlock = false;
	void FixedUpdate() {
		if (rb.velocity.x == 0 && collidingWithBlock) Camera.main.GetComponent<Manager>().onLose();

		if (rb.velocity.x != speed) rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);

        if (jumpRequest) {
			rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            jumpRequest = false;
		}

        collidingWithBlock = false;
	}

	void OnCollisionStay(Collision collision) {
		if (collision.transform.tag == "block") collidingWithBlock = true;
	}
}
