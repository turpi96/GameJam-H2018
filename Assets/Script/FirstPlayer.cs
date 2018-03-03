using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayer : Player {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (playerState == PlayerState.Ingame) {
			float x = 0;
			float y = 0;
			if (Mathf.Abs (Input.GetAxis ("Player1_Horizontal")) > 0.2f)
				x = Input.GetAxis ("Player1_Horizontal");
			if (Mathf.Abs (Input.GetAxis ("Player1_Vertical")) > 0.2f)
				y = Input.GetAxis ("Player1_Vertical");


			transform.Translate (new Vector2 (x, y).normalized * cursorSpeed * Time.deltaTime);
		}
		Camera cam = FindObjectOfType<Camera> ();
		Vector3 pos = cam.WorldToScreenPoint (transform.position);
		if (pos.x > cam.pixelWidth / 2.0f) {
			pos.x = cam.pixelWidth / 2.0f;
	
		} else if (pos.x > cam.pixelWidth) {
			pos.x = cam.pixelWidth;
		} else if (pos.y > cam.pixelHeight) {
			pos.y = cam.pixelHeight;
		} else if (pos.y < 0) {
			pos.y = 0;
		} else if (pos.x < 0) {
			pos.x = 0;
		}
		transform.position = cam.ScreenToWorldPoint (pos);
	}
}
