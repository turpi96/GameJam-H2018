using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Casting, HasTeam {

	private int timeleft= 2;
    

	private AudioSource myAudio;

	private bool play = true;
	private bool toggleChange = true;
	private bool activateTimer = true;

    private bool spriteVisible = true;
    private bool canFlash = true;

    public string team;
    public int cost;

    public int attack;
    
    public Sprite explosionSprite;

	private List<GameObject> explosionList;

	void Start () {

		explosionList = new List<GameObject>();
        

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
			
			foreach (GameObject g in explosionList) {
				if(team != g.GetComponent<HasTeam>().getTeam()){
                    g.GetComponent<CanBeHurt>().Hurt(attack);
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
		if (state == CastingState.inGame && (other.tag == "Unit" || other.tag == "Turret"))
		{
			explosionList.Add(other.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (state == CastingState.inGame && (other.tag == "Unit" || other.tag == "Turret"))
		{
			explosionList.Remove(other.gameObject);
		}
	}
}