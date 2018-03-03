using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	private int timeleft= 2;

	void Start () {
		StartCoroutine ("explosion");
	}

	void Update(){
		if (timeleft <= 0) {
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
		this.name = this.transform.parent.name;
		Destroy (gameObject, 1);
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.name != this.name) {
			Destroy (collision.gameObject);
		}
	}
}

