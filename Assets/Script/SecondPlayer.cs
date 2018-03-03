using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlayer : Player {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (playerState == PlayerState.Ingame) {
			float x = 0;
			float y = 0;
			if (Mathf.Abs (Input.GetAxis ("Player2_Horizontal")) > 0.2f)
				x = Input.GetAxis ("Player2_Horizontal");
			if (Mathf.Abs (Input.GetAxis ("Player2_Vertical")) > 0.2f)
				y = Input.GetAxis ("Player2_Vertical");

			transform.Translate (new Vector2 (x, y).normalized * cursorSpeed * Time.deltaTime);
		}

		Camera cam = FindObjectOfType<Camera> ();
		Vector3 pos = cam.WorldToScreenPoint (transform.position);
		if (pos.x < cam.pixelWidth / 2.0f) {
			pos.x = cam.pixelWidth / 2.0f;
			transform.position = cam.ScreenToWorldPoint (pos);
		}
	}
}
