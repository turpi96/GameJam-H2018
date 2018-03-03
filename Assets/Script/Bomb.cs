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
		//OnDrawGizmosSelected ();
		Destroy (gameObject, 1);
	}

	/*public void OnDrawGizmosSelected(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere (transform.position, 5);
	}*/
}

