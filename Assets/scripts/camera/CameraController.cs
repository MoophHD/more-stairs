using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject toFollow;
    private Player player;
    private Vector3 offset;
    private bool isFrozen = false;
    public void freeze(bool freeze) {
        isFrozen = freeze;
    }
    void Start() {
        offset = transform.position - toFollow.transform.position;
        player = toFollow.GetComponent<Player>();
    }

    public void reset() {
        transform.position = player.startPos + offset;
    }

    private float lastY;
    Vector3 toFollowPos;
    void FixedUpdate() {
        toFollowPos = toFollow.transform.position;

        transform.position = new Vector3(
            toFollowPos.x,
            transform.position.y - offset.y,
            toFollowPos.z) + offset;
    }

    public void flip() {
        Camera.main.projectionMatrix = Camera.main.projectionMatrix * Matrix4x4.Scale(new Vector3(-1, 1, 1));
    }
    private Vector3 velocity = Vector3.zero;
    public bool stabilisingY = false;
    void Update() {
        if (isFrozen) return;

        if (stabilisingY) {
            Vector3 toFollowPos = toFollow.transform.position;
            if ( Mathf.Round(transform.position.y) == Mathf.Round(toFollowPos.y)) {
                stabilisingY = false;
                return;
            }

            transform.position = Vector3.SmoothDamp(transform.position, toFollowPos + offset, ref velocity, 0.3f);
        }
    }
}
