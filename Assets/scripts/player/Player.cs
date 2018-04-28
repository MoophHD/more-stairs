using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {
	public float speed = 3.6f;
    public float jumpVelocity = 24f;

    private Vector3 _currentVelocity;

	private bool jumpRequest = false;
	public Vector3 startPos;
    private Rigidbody rb;
    private Transform tr;
	private bool collidingWithBlock = false;
    private CameraController myCamera;
	private CustomGravity gravity;
    public float fallScale = 13.5f;
    public float jumpScale = 14.25f;
    public bool isJumping = false;

    public float jumpHeight;

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
        jumpHeight = (jumpVelocity * jumpVelocity) / (2 * jumpScale * -CustomGravity.globalGravity);
        myCamera = Camera.main.GetComponent<CameraController>();
		gravity = GetComponent<CustomGravity>();
		rb = GetComponent<Rigidbody>();
		tr = GetComponent<Transform>();

        startPos = tr.position;
	}

	private void jump() {
        rb.velocity += Vector3.up * jumpVelocity;
    }
    void Update() {
        if (!UnityEngine.Rendering.SplashScreen.isFinished) return;

        if (gravity.gravityScale == 0) return;
        if (Input.GetMouseButtonDown(0)) {
            //deny requests on 1st part of a jump
            if (Mathf.Round(rb.velocity.y) > 0) return;
            if (IsPointerOverUIObject()) return;
            jumpRequest = true;
        }
    }

    void FixedUpdate()
    {
        if (!collidingWithBlock || Mathf.Round(rb.velocity.y) != 0)
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }

        myCamera.stabilisingY = !isJumping;

        if (jumpRequest
            && (!isJumping)
            && gravity.gravityScale != 0)
        {
            jump();
            isJumping = true;

            jumpRequest = false;
        }

        if (rb.velocity.x != speed) rb.velocity = new Vector3(speed, rb.velocity.y, 0f);

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
        if (collider.tag == "playerTrigger") {
            collidingWithBlock = true;
        } else if (collider.tag == "loseTrigger") {
            Camera.main.GetComponent<Manager>().onLose();
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
