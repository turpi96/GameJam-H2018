using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Casting, HasTeam {

	private int timeleft= 2;

	private SpriteRenderer renderer;
	private AudioSource audio;

	public AudioSource myAudio;

	private bool play = true;
	private bool toggleChange = true;

	public string team;

	void Start () {


		audio = GetComponent<AudioSource> ();
		renderer = GetComponent<SpriteRenderer> ();

		myAudio = GetComponent<AudioSource> ();


		StartCoroutine ("explosion");
	}

	new void Update () {
		base.Update ();
		if (state == CastingState.inGame) {
			
			if (timeleft <= 0) {
				StopCoroutine ("explosion");
				waitAndExplode ();
			}
		}
	}

	public string getTeam(){
		return team;
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

