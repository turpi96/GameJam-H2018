﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayer : Player {
	public Building dumbTower;
	public Unit dumbUnit;
	public Transform spawnPoint;
	// Use this for initialization
	new public void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	new public void Update () {
		base.Update ();
		if (Input.GetKeyDown (KeyCode.O) && playerState == PlayerState.Ingame) {
			changeState (PlayerState.Building);
			currentlyBuilding = Instantiate (dumbTower, new Vector3(transform.position.x,transform.position.y,1),transform.rotation);
			gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			currentlyBuilding.changeState (Building.BuildingState.inConstruction);
			currentlyBuilding.team = "p1";
		} else if (Input.GetKeyDown (KeyCode.O) && playerState == PlayerState.Building && currentlyBuilding.canBuild) {
			changeState (PlayerState.Ingame);
			currentlyBuilding.changeState (Building.BuildingState.inGame);
			currentlyBuilding = null;
			gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		}

		if (Input.GetKeyDown (KeyCode.U) && playerState == PlayerState.Ingame) { 
			changeState (PlayerState.CastingSpell);
			Debug.Log (PlayerState.CastingSpell);
			currentlyCasting = Instantiate (bomb, new Vector3(transform.position.x,transform.position.y,1),transform.rotation) as Casting;
			gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			currentlyCasting.changeState(Casting.CastingState.isTargeting);
		} 
		else if (Input.GetKeyDown (KeyCode.U) && playerState == PlayerState.CastingSpell && currentlyCasting.canCast) {
			changeState (PlayerState.Ingame);
			currentlyCasting.changeState (Casting.CastingState.inGame);
			currentlyCasting = null;
			gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		}

		if (Input.GetKeyDown (KeyCode.J) && playerState == PlayerState.Ingame) { 
			Unit unit = Instantiate (dumbUnit, spawnPoint.position, spawnPoint.rotation);
			unit.GetComponent<MoveOnPath> ().PathToFollow = pathToFollow;
		}
	}

	public  override void checkInput(){
		float x = 0;
		float y = 0;
		if (Mathf.Abs (Input.GetAxis ("Player1_Horizontal")) > 0.2f)
			x = Input.GetAxis ("Player1_Horizontal");
		if (Mathf.Abs (Input.GetAxis ("Player1_Vertical")) > 0.2f)
			y = Input.GetAxis ("Player1_Vertical");
		switch(playerState){

			case PlayerState.Ingame:
				transform.Translate (new Vector2 (x, y).normalized * cursorSpeed * Time.deltaTime);
				break;
			case PlayerState.Building:
				if (currentlyBuilding != null) {
					transform.Translate (new Vector2 (x, y).normalized * cursorSpeed * Time.deltaTime);
					currentlyBuilding.transform.position = transform.position;
				}
				else
					Debug.Log ("NO BUILDING FOUND");
				break;
		case PlayerState.CastingSpell:
			if (currentlyCasting != null) {
				transform.Translate (new Vector2 (x, y).normalized * cursorSpeed * Time.deltaTime);
				currentlyCasting.transform.position = transform.position;
			}
			break;
			 default:
				transform.Translate (new Vector2 (x, y).normalized * cursorSpeed * Time.deltaTime);
				break;
		}

	}

	public override void checkPosition(){
			Camera cam = FindObjectOfType<Camera> ();

			Vector2 halfSize;
			switch (playerState) {

			case PlayerState.Building:
				halfSize.x = currentlyBuilding.GetComponent<SpriteRenderer> ().bounds.size.x * 10.8f;
				halfSize.y = currentlyBuilding.GetComponent<SpriteRenderer> ().bounds.size.y * 10.8f;
				break;
			case PlayerState.CastingSpell:
					halfSize.x = currentlyCasting.GetComponent<SpriteRenderer> ().bounds.size.x * 10.8f;
					halfSize.y = currentlyCasting.GetComponent<SpriteRenderer> ().bounds.size.y * 10.8f;
					break;
			default:
				halfSize.x = GetComponent<SpriteRenderer> ().bounds.size.x * 10.8f;
				halfSize.y = GetComponent<SpriteRenderer> ().bounds.size.y * 10.8f;
				break;
			}

			Vector3 pos = cam.WorldToScreenPoint (transform.position);
			if (pos.x + halfSize.x > cam.pixelWidth / 2.0f) {
				pos.x = cam.pixelWidth / 2.0f - halfSize.x;

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
		if (Input.GetMouseButtonDown (0) && playerState == PlayerState.CastingSpell) {
			Vector3 mousePos = Input.mousePosition;
			Vector3 posCam = cam.ScreenToWorldPoint (mousePos);
			posCam.z = 0;
			Instantiate (bomb,posCam , Quaternion.identity,transform);
		}
	}

	public override void spawnArrow(){
		/*if (Input.GetMouseButtonDown (1)) {
			Vector3 mousePos = Input.mousePosition;
			Vector3 posCam = cam.ScreenToWorldPoint (mousePos);
			posCam.z = 0;
			Instantiate (bomb, posCam, Quaternion.identity, transform);
		}*/
	}
}
