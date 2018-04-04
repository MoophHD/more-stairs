using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
	public GameObject track;
	public bool spawned;

	void OnTriggerEnter(Collider other) {
		if (spawned) return;

		
		if (other.tag == "player") {
			spawned = true;
		}
	}
	void Start () {
		
	}
	
	void Update () {
		
	}
}
