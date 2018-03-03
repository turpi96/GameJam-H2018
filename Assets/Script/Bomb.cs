using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	private int timeleft= 2;
	public Material matBomb;
	private Renderer renderer;
	public AudioSource audio;
	private bool play = true;
	private bool toggleChange = true;

	void Start () {
		renderer = GetComponent<MeshRenderer> ();
		matBomb = new Material (matBomb);
		renderer.material = matBomb;

		audio = GetComponent<AudioSource> ();

		this.tag = this.transform.parent.tag;

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
		if (play == true && toggleChange == true) {
			audio.Play ();
			toggleChange = false;
		}
		Destroy (gameObject, 1);
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag != this.tag) {
			Destroy (collision.gameObject);
		}
	}
}

