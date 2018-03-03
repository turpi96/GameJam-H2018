using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlayer : Player {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	new public void Update () {
		base.Update ();
	}

	public override void checkInput(){
		switch(playerState){
			case PlayerState.Ingame:
				float x = 0;
				float y = 0;
				if (Mathf.Abs (Input.GetAxis ("Player2_Horizontal")) > 0.2f)
					x = Input.GetAxis ("Player2_Horizontal");
				if (Mathf.Abs (Input.GetAxis ("Player2_Vertical")) > 0.2f)
					y = Input.GetAxis ("Player2_Vertical");

				transform.Translate (new Vector2 (x, y).normalized * cursorSpeed * Time.deltaTime);
				break;
			}
	}
	public override void checkPosition(){
		Camera cam = FindObjectOfType<Camera> ();
		Vector2 halfSize;
		halfSize.x = GetComponent<SpriteRenderer> ().sprite.rect.x / 2.0f;
		halfSize.y = GetComponent<SpriteRenderer> ().sprite.rect.y / 2.0f;
		//GetComponent<SpriteRenderer> ().sprite.rect.x;
		Vector3 pos = cam.WorldToScreenPoint (transform.position);
		if (pos.x - halfSize.x < cam.pixelWidth / 2.0f) {
			pos.x = cam.pixelWidth / 2.0f + halfSize.x;

		}
		if (pos.x + halfSize.x > cam.pixelWidth) {
			pos.x = cam.pixelWidth - halfSize.x;
		}
		if (pos.y + halfSize.y > cam.pixelHeight) {
			pos.y = cam.pixelHeight - halfSize.y;
		}
		if (pos.y < 0 + halfSize.y) {
			pos.y = 0 + halfSize.y;
		}
		if (pos.x < 0 + halfSize.x) {
			pos.x = 0 + halfSize.x;
		}
		transform.position = cam.ScreenToWorldPoint (pos);
	}

	public override void spawnBomb(){
		/*if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("left Pressed");
		}*/
	}
}
