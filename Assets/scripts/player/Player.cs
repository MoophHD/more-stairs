using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public float speed = 3.25f;
    public float jumpVelocity = 23.5f;

    private Vector3 _currentVelocity;

	private bool jumpRequest = false;
	public Vector3 startPos;
    private Rigidbody rb;
    private Transform tr;
	private bool collidingWithBlock = false;
    private CameraController myCamera;

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
        myCamera = Camera.main.GetComponent<CameraController>();
		gravity = GetComponent<CustomGravity>();
		rb = GetComponent<Rigidbody>();
		tr = GetComponent<Transform>();

        startPos = tr.position;
	}

	private void jump() {
        rb.velocity += Vector3.up * jumpVelocity;
    }
	private bool lastFrameJump = false;
    public float fallScale = 10f;
	public float jumpScale = 12f;
	public bool isJumping = false;
	// void Update() {

	// 	if (!collidingWithBlock || Mathf.Round(rb.velocity.y) != 0 ) {
	// 		isJumping = true; 
	// 	} else {
    //         isJumping = false;
	// 	}
    //     if (lastFrameJump && rb.velocity.y <= 2) jump();
    //     lastFrameJump = false;
		
    //     if (Input.GetMouseButtonDown(0) 
    //     	&& ( !isJumping )
    //         && gravity.gravityScale != 0) {
    //         jump();
    //         isJumping = true;
    //         lastFrameJump = true;
    //     } else {
    //     	if (Input.GetMouseButtonDown(0)) {
    //     		print("jump failed " + id++);
    //             if (Mathf.Round(rb.velocity.y) != 0) print("velocity is not stable " + rb.velocity.y);
    //     		if (!collidingWithBlock) print("not colliding with a block");
    //     	}
	// 	}


    //     collidingWithBlock = false;
    // }
	// void FixedUpdate() {
	// 	// if (rb.velocity.x == 0 && collidingWithBlock) Camera.main.GetComponent<Manager>().onLose();
	// 	if (rb.velocity.x != speed) rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
	
    //     if (rb.velocity.y < 0) {
    //     	gravity.gravityScale = fallScale;
    //     } else if (rb.velocity.y > 0) {
    //         gravity.gravityScale = jumpScale;
    //     } else {
    //     	if (gravity.gravityScale != 1) gravity.gravityScale = 1;
    //     }

	// }



    void Update() {
        if (Input.GetMouseButtonDown(0)) jumpRequest = true;
    }

    void FixedUpdate()
    {
		if (!collidingWithBlock || Mathf.Round(rb.velocity.y) != 0)
        {
            isJumping = true;
        }
        else
        {
            myCamera.stabilising = true;
            isJumping = false;

        }
        if (lastFrameJump && rb.velocity.y <= 2) jump();
        lastFrameJump = false;

        if (jumpRequest
            && (!isJumping)
            && gravity.gravityScale != 0)
        {
            jump();
            isJumping = true;
            lastFrameJump = true;

            jumpRequest = false;
        }

        if (rb.velocity.x != speed) rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);

        if (rb.velocity.y < 0)
        {
            gravity.gravityScale = fallScale;
        }
        else if (rb.velocity.y > 0)
        {
            gravity.gravityScale = jumpScale;
        }
        else
        {
            if (gravity.gravityScale != 1) gravity.gravityScale = 1;
        }

    }

	void OnCollisionStay(Collision collision) {
        if (collision.transform.tag == "block") collidingWithBlock = true;
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "block") collidingWithBlock = true;
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "playerTrigger") collidingWithBlock = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        print("trigger enter " + collider.tag);
        if (collider.tag == "playerTrigger") {
            collidingWithBlock = true;
        } else if (collider.tag == "loseTrigger") {
            Camera.main.GetComponent<Manager>().onLose();
        }
    }
}
