using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
	public enum BuildingState{
		inGame = 1,
		inConstruction = 2
	}
	bool canBuild = true;
	public List<Collider2D> colliders;

	protected BuildingState state = BuildingState.inGame;

	public void changeState(BuildingState newState){
		state = newState;
		if (newState == BuildingState.inConstruction) {
			GetComponent<SpriteRenderer> ().color = Color.green;
			canBuild = true;
		} else
			GetComponent<SpriteRenderer> ().color = Color.white;

	}
	// Use this for initialization
	void Start () {
		
	}

	public void setCanBuild(bool val){
		if (val) 
			GetComponent<SpriteRenderer> ().color = Color.green;
		else
			GetComponent<SpriteRenderer> ().color = Color.red;
		canBuild = val;


		
	}

	/*public void OnTriggerEnter2D(Collider2D collider){
		if (state == BuildingState.inConstruction && collider.gameObject.name == "Path") {
			GetComponent<SpriteRenderer> ().color = Color.red;
		}
	}*/
	// Update is called once per frame
	void Update () {
		if (state == BuildingState.inConstruction) {
			if (colliders.Count == 0)
				setCanBuild (true);
			else
				setCanBuild (false);
		}
		if (Input.GetKeyDown (KeyCode.I))
			changeState (BuildingState.inConstruction);
	}
}
