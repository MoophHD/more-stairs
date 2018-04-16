using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	const float speed = 3.75f;
	const float jumpVelocity = 21.5f;

    private Vector3 _currentVelocity;

	private bool jumpRequest;
	public Vector3 startPos;
    private Rigidbody rb;
    private Transform tr;
	private bool collidingWithBlock = false;

	private CustomGravity gravity;
    public void reset(){
		tr.position = startPos;
		rb.velocity = Vector3.zero;
        gravity.gravityScale = 0;

		rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
	}

    public void start()
    {
		rb.constraints = RigidbodyConstraints.FreezeRotation;
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

	private void jump() {
        rb.velocity += Vector3.up * jumpVelocity;
		print(rb.velocity);
    }

	int id = 0;
	private bool lastFrameJump = false;
	private float fallScale = 7f;
	private float jumpScale = 8.5f;
	public bool isJumping = false;
	void Update() {
		if (!collidingWithBlock || Mathf.Round(rb.velocity.y) != 0 ) {
			isJumping = true; 
		} else {
            isJumping = false;
		}
        if (lastFrameJump && rb.velocity.y <= 2) jump();
        lastFrameJump = false;
		
        if (Input.GetMouseButtonDown(0) 
        	&& ( !isJumping )
            && gravity.gravityScale != 0) {
            jump();
            isJumping = true;
            lastFrameJump = true;
        } else {
        	if (Input.GetMouseButtonDown(0)) {
        		print("jump failed " + id++);
                if (Mathf.Round(rb.velocity.y) != 0) print("velocity is not stable " + rb.velocity.y);
        		if (!collidingWithBlock) print("not colliding with a block");
        	}
		}


        collidingWithBlock = false;
    }
	void FixedUpdate() {
		if (rb.velocity.x == 0 && collidingWithBlock) Camera.main.GetComponent<Manager>().onLose();
		if (rb.velocity.x != speed) rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
	
        if (rb.velocity.y < 0) {
        	gravity.gravityScale = fallScale;
        } else if (rb.velocity.y > 0) {
            gravity.gravityScale = jumpScale;
        } else {
        	if (gravity.gravityScale != 1) gravity.gravityScale = 1;
        }

	}

	// void OnCollisionStay(Collision collision) {
    //     if (collision.transform.tag == "block") collidingWithBlock = true;
	// }

    // void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.transform.tag == "block") collidingWithBlock = true;
    // }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "playerTrigger") collidingWithBlock = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "playerTrigger") collidingWithBlock = true;
    }
}
