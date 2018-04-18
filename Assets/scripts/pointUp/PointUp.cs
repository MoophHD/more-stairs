using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointUp : MonoBehaviour {
	private float suicideAfter = 25f;
	void OnTriggerEnter(Collider collider) {
		if (collider.tag == "player") {
			Camera.main.GetComponent<Score>().addPointUp();
			Destroy(gameObject);
		} else if (collider.tag == "blockTrigger") {
            Invoke("suicide", suicideAfter);
		}
	}

    void suicide()
    {
        Destroy(gameObject);
    }

}
