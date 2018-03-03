using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour {

	public enum CastingState{
		inGame = 1,
		isTargeting = 2
	}

	public bool canCast = true;
	public List<Collider2D> colliders;
	public CastingState state = CastingState.inGame;

	public void changeState(CastingState newState){
		state = newState;
		if (newState == CastingState.isTargeting) {
			GetComponent<SpriteRenderer> ().color = Color.gray;
			canCast = true;
		}
	}

	// Use this for initialization
	void Start () {
		
	}

	public void setCanCast(bool val){
		if (val)
			GetComponent<SpriteRenderer> ().color = Color.green;
		else
			GetComponent<SpriteRenderer> ().color = Color.red;
		canCast = val;
	}
	
	// Update is called once per frame
	public void Update () {
		if (state == CastingState.isTargeting) {
			if (colliders.Count == 0)
				setCanCast (true);
			else
				setCanCast (false);
		}
		if (Input.GetKeyDown (KeyCode.I))
			changeState (CastingState.isTargeting);
	}
}