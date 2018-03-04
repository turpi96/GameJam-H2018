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
    private bool canDoExit = true;

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
				
				//StopCoroutine ("explosion");
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

            canDoExit = false;
            for (int i = 0; i< explosionList.Count;i++)
            {
                if (team != explosionList[i].GetComponent<HasTeam>().getTeam())
                {
                    explosionList[i].GetComponent<CanBeHurt>().Hurt(attack);
                }
            }
			explosionList.Clear();
			myAudio.Play ();
			toggleChange = false;
		}
		Destroy (gameObject, 1);
	}

	 void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Unit" || other.tag == "Turret")
		{
            if(!explosionList.Contains(other.gameObject))
            {
                //Debug.Log("Add : " + other.gameObject.name);
                
                explosionList.Add(other.gameObject);
               // Debug.Log("LIST : " + explosionList.Count);
            }
			    
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if ((other.tag == "Unit" || other.tag == "Turret") && canDoExit)
		{
            if (explosionList.Contains(other.gameObject))
            {
                //Debug.Log("Remove : " + other.gameObject.name);
                explosionList.Remove(other.gameObject);
                //Debug.Log("LIST : " + explosionList.Count);
            }
                
		}
	}
}