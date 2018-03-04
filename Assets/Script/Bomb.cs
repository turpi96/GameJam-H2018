using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Casting, HasTeam {

	private int timeleft= 2;
    
	private AudioSource audioS;

	public AudioSource myAudio;

	private bool play = true;
	private bool toggleChange = true;
	private bool activateTimer = true;

    private bool spriteVisible = true;
    private bool canFlash = true;

    public string team;
    public int cost;
    
    public Sprite explosionSprite;

	private List<Unit> explosionList;

	void Start () {

		explosionList = new List<Unit>();

		audioS = GetComponent<AudioSource> ();

		myAudio = GetComponent<AudioSource> ();


	}

	new void Update () {
		base.Update ();

		if (state == CastingState.inGame) {
			if (activateTimer) {
				StartCoroutine ("explosion");
                StartCoroutine(flashingBomb());
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
			yield return new WaitForSeconds (0.5f);
			timeleft--;
		}
	}

    IEnumerator flashingBomb()
    {
        while (canFlash)
        {
            if(spriteVisible)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                spriteVisible = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = true;
                spriteVisible = true;
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void waitAndExplode(){
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = explosionSprite;
        canFlash = false;
        GetComponent<SpriteRenderer>().enabled = false;
        //rendererSp.color = Color.yellow;
        if (play == true && toggleChange == true) {
			
			foreach (Unit u in explosionList) {
				if(this.getTeam() != u.getTeam()){
					Destroy(u.gameObject);
				}
			}
			explosionList = null;
			myAudio.Play ();
			toggleChange = false;
		}
		Destroy (gameObject, 1);
	}

	 void OnTriggerEnter2D(Collider2D other)
	{
		if (state == CastingState.inGame && other.GetComponent<Unit>())
		{
			explosionList.Add(other.GetComponent<Unit>());
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (state == CastingState.inGame && other.GetComponent<Unit>() )
		{
			explosionList.Remove(other.GetComponent<Unit>());
		}
	}
}