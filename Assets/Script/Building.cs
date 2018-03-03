using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
	public enum BuildingState{
		inGame = 1,
		inConstruction = 2
	}

	protected BuildingState state;

	public void changeState(BuildingState newState){
		state = newState;
		if (newState == BuildingState.inConstruction) {
			GetComponent<SpriteRenderer> ().color = Color.green;
		} else
			GetComponent<SpriteRenderer> ().color = Color.white;

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.I))
			changeState (BuildingState.inConstruction);
	}
}
