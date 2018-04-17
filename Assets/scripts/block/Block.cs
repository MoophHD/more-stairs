using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
	public bool isSpawnTrigger = true;
	public bool spawned = false;
	const int lifeTime = 25;

	void OnTriggerEnter(Collider other) {
        if (spawned) return;

		if (other.tag == "blockTrigger") {
			spawned = true;
			if (isSpawnTrigger) {
                Camera.main.GetComponent<Manager>().spawn();
                Camera.main.GetComponent<Score>().add();
			} 

            Invoke("suicide", lifeTime);
		}
	}
	void suicide() {
		Destroy(gameObject);
	}
}
