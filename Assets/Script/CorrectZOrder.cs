using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectZOrder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject g in FindObjectsOfType<GameObject>()) {
			if (g.GetComponent<Player> () == null && g.GetComponent<Camera>() == null && g.tag != "Background") {
				Vector3 temp = g.transform.position;
				temp.z = g.transform.position.y;
				g.transform.position = temp;

			}

		}
			
	}
}
