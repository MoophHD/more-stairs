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

	private CustomGravity gravity;
    public void reset(){
		tr.position = startPos;
		rb.velocity = Vector3.zero;
        gravity.gravityScale = 0;

		rb.constraints = RigidbodyConstraints.FreezePosition;
	}

    public void start()
    {
		rb.constraints = RigidbodyConstraints.None;
        Vector3 dir = Vector3.right;
        rb.velocity = dir * speed;
        gravity.gravityScale = 1;
    }

	void Awake() {
		gravity = GetComponent<CustomGravity>();
		rb = GetComponent<Rigidbody>();
		tr = GetComponent<Transform>();

        startPos = tr.position;
	}

	void Update() {
        if (Input.GetMouseButtonDown(0) && gravity.gravityScale != 0)
        {
            jumpRequest = true;
        }
	}

	private bool collidingWithBlock = false;
	void FixedUpdate() {
		if (rb.velocity.x == 0 && collidingWithBlock) Camera.main.GetComponent<Manager>().onLose();

		if (rb.velocity.x != speed) rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);

        if (jumpRequest && collidingWithBlock) {
			rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            jumpRequest = false;
		}

        collidingWithBlock = false;
	}

	void OnCollisionStay(Collision collision) {
		if (collision.transform.tag == "block") collidingWithBlock = true;
	}
}
