using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	private int timeleft= 2;
	private bool alreadyIn = false;

	void Start () {
		StartCoroutine ("explosion");
		
	}

	void Update(){
		if (timeleft <= 0 && !alreadyIn) {
			StopCoroutine ("explosion");
			waitAndExplode ();
		}
	}

	IEnumerator explosion(){
		while (true) {
			yield return new WaitForSeconds (1);
			timeleft--;
		}
	}
	public void waitAndExplode(){
		alreadyIn = true;
		Debug.Log ("Yooo");
		//insert fancy explosion here;
		Destroy (gameObject, 1);
	}
}

