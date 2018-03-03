﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		//Debug.Log (other.tag);
		if (other.tag == "Turret") {
			other.gameObject.GetComponent<Turret> ().colliders.Add(GetComponent<Collider2D>());
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Turret") {
			other.gameObject.GetComponent<Turret> ().colliders.Remove(GetComponent<Collider2D>());
		}

	}
}