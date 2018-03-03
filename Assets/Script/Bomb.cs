using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	private int timeleft= 2;

	private SpriteRenderer renderer;
	private AudioSource audio;

	public Material matBomb;
	private Renderer myRenderer;
	public AudioSource myAudio;

	private bool play = true;
	private bool toggleChange = true;

	void Start () {


		audio = GetComponent<AudioSource> ();
		renderer = GetComponent<SpriteRenderer> ();

		myRenderer = GetComponent<MeshRenderer> ();
		matBomb = new Material (matBomb);
		myRenderer.material = matBomb;

		myAudio = GetComponent<AudioSource> ();


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
		renderer.color = Color.yellow;
		if (play == true && toggleChange == true) {
			myAudio.Play ();
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

