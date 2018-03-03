using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	public enum TowerState{
		inGame = 1,
		inConstruction = 2
	}

	TowerState state;
    public int health;

	// Use this for initialization
	void Start () {
		
	}
	public void changeState(TowerState newState){
		state = newState;
		if (newState == TowerState.inConstruction) {
			GetComponent<SpriteRenderer> ().color = Color.green;
		} else
			GetComponent<SpriteRenderer> ().color = Color.white;

	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.I))
			changeState (TowerState.inConstruction);
	}

    public void Hurt(int amount)
    {
        health -= amount;

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //LA GAME EST FUCKIN FINI
        //WIN() or LOSE()
    }
}
