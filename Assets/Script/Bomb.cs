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
	private bool activateTimer = true;

	public string team;

	private List<GameObject> explosionList;

	void Start () {

		explosionList = new List<GameObject>();

		audio = GetComponent<AudioSource> ();
		renderer = GetComponent<SpriteRenderer> ();

		myAudio = GetComponent<AudioSource> ();


	}

	new void Update () {
		base.Update ();
		if (state == CastingState.inGame) {
			if (activateTimer) {
				StartCoroutine ("explosion");
				activateTimer = false;
			}
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

	 void OnTriggerEnter2D(Collider2D other)
	{
		if (state == CastingState.inGame && other.GetComponent<Unit>())
		{
			explosionList.Add(other.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (state == CastingState.inGame && other.GetComponent<Unit>() )
		{
			explosionList.Remove(other.gameObject);

		}
	}
}
