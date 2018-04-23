using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointUp : MonoBehaviour {
	private float suicideAfter = 25f;
	private PopUp popUpText;
	private Material material;
	void OnTriggerEnter(Collider collider) {
		if (collider.tag == "player") {
			Camera.main.GetComponent<Score>().addPointUp();


            material = GetComponent<Renderer>().material;

            material.SetFloat("_Mode", 2f);
            material.DisableKeyword("_EMISSION");
            material.color = new Color(0,0,0,0);

            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000;
            popUpText = PopUpController.createPopUp(transform.position);

            // Destroy(gameObject, popUpText.animationLength);
        } else if (collider.tag == "blockTrigger") {
            Destroy(gameObject, suicideAfter);
		}
	}

	void Update() {
		if (popUpText) popUpText.transform.position = Camera.main.WorldToScreenPoint(transform.position);
	}
}
