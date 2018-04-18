using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject toFollow;
    private Player player;
    private Vector3 offset;

    void Start() {
        offset = transform.position - toFollow.transform.position;
        player = toFollow.GetComponent<Player>();
    }

    public void reset() {
        transform.position = player.startPos + offset;
    }

    private float lastY;
    void FixedUpdate() {
        Vector3 toFollowPos = toFollow.transform.position;

        transform.position = new Vector3(
            toFollowPos.x,
            transform.position.y - offset.y,
            toFollowPos.z) + offset;
    }
    private Vector3 velocity = Vector3.zero;
    public bool stabilisingY = false;
    void Update() {
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
