using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	private int timeleft= 2;
	public Material matBomb;

	void Start () {
		this.tag = this.transform.parent.tag;
		Debug.Log (this.tag);
		if (this.tag == "Player1") {
			matBomb.color = Color.red;
		}
		if (this.tag == "Player2") {
			matBomb.color = Color.green;
		}
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
		matBomb.color = Color.yellow;
		Destroy (gameObject, 1);
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag != this.tag) {
			Destroy (collision.gameObject);
		}
	}
}

