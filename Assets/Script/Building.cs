using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
	public enum BuildingState{
		inGame = 1,
		inConstruction = 2
	}
	public bool canBuild = true;
	public List<Collider2D> colliders;

	public BuildingState state = BuildingState.inGame;

	public void changeState(BuildingState newState){
		state = newState;
		if (newState == BuildingState.inConstruction) {
			GetComponent<SpriteRenderer> ().color = Color.green;
			canBuild = true;
			Component[] c = GetComponentsInChildren<CircleCollider2D> ();
			foreach (CircleCollider2D cc in c)
				if(cc.name == "RangeRadius")   
					cc.enabled = false;
					
		} else {
			GetComponent<SpriteRenderer> ().color = Color.white;
			Component[] c = GetComponentsInChildren<CircleCollider2D> ();
			foreach (CircleCollider2D cc in c)
				if(cc.name == "RangeRadius") 
					cc.enabled = true;
		}

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
	public void Update () {
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
