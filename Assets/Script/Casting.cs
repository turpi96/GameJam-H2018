using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour {

	public enum CastingState{
		inGame = 1,
		isTargeting = 2
	}

	public bool canCast = true;
	public CastingState state = CastingState.inGame;

	public void changeState(CastingState newState){
		state = newState;
		if (newState == CastingState.isTargeting) {
			canCast = true;
		}
	}

	// Use this for initialization
	void Start () {
		
	}

	public void setCanCast(bool val){
		canCast = val;
	}
	
	// Update is called once per frame
	public void Update () {
		if (state == CastingState.isTargeting) {
				setCanCast (true);
		}
		if (Input.GetKeyDown (KeyCode.I))
			changeState (CastingState.isTargeting);
	}
}