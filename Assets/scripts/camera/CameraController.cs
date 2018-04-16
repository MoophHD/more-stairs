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

    private float lastY;
    void Update() {
        //~stable velocity
        bool followY = !player.isJumping;
        Vector3 toFollowPos = toFollow.transform.position;

        transform.position = new Vector3(
            toFollowPos.x,
            followY ? toFollowPos.y : transform.position.y - offset.y,
            toFollowPos.z) + offset;
    }
}
