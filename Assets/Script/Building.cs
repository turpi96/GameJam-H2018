using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour , HasTeam , CanBeHurt, HasHealth {
	public enum BuildingState{
		inGame = 1,
		inConstruction = 2
	}
	public string team;
	public int cost= 100;
	public float health;
	private float maxHealth;
	public int defense = 5;
    public int value;

	public bool canBuild = true;
	public List<Collider2D> colliders;

	public BuildingState state = BuildingState.inGame;

	public string getTeam(){
		return team;
	}
	public float getHealth(){
		return health;
	}
	public float getMaxHealth(){
		return maxHealth;
	}
	public void changeState(BuildingState newState){
		state = newState;
		if (newState == BuildingState.inConstruction) {
			GetComponent<SpriteRenderer> ().color = Color.green;
			canBuild = true;
			Component[] c = GetComponentsInChildren<CircleCollider2D> ();
			foreach (CircleCollider2D cc in c)
				if(cc.name == "RangeRadius")   
					cc.enabled = false;
					
		} else {
			GetComponent<SpriteRenderer> ().color = Color.white;
			Component[] c = GetComponentsInChildren<CircleCollider2D> ();
			foreach (CircleCollider2D cc in c)
				if(cc.name == "RangeRadius") 
					cc.enabled = true;
		}

	}
	// Use this for initialization
	public void Start () {
        maxHealth = health;
	}

	public void setCanBuild(bool val){
		GetComponent<SpriteRenderer> ().color = val ? Color.green : Color.red;
		canBuild = val;


		
	}

	public void OnTriggerEnter2D(Collider2D collider){
		if (collider.tag == "Turret") {
			if(state == BuildingState.inConstruction)
				colliders.Add (GetComponent<Collider2D> ());
		}
	}

	public void OnTriggerExit2D(Collider2D collider){
		if (collider.tag == "Turret") {
			if(state == BuildingState.inConstruction)
				colliders.Remove (GetComponent<Collider2D> ());
		}

	}
	// Update is called once per frame
	public void Update () {
		if (state == BuildingState.inConstruction) {
			if (colliders.Count == 0)
				setCanBuild (true);
			else
				setCanBuild (false);
		}

	}

    public void Hurt(int amount)
    {

        GetComponent<SpriteRenderer>().color = new Color(0.604f, 0.13f, 0.13f);
        StartCoroutine(TimerWhiteColor());

        health = health - (amount * (1 - defense / 100));

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    IEnumerator TimerWhiteColor()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
