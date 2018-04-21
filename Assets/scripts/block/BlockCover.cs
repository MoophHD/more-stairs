using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCover : MonoBehaviour
{
    const int destroyDelay = 25;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "player") Destroy(gameObject, destroyDelay);
    }
}
