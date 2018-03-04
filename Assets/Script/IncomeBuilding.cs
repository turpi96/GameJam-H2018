using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomeBuilding : Building {
	float timerIncome = 3.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
		timerIncome -= Time.deltaTime;
		if (timerIncome < 0) {
			timerIncome = 3.0f;
			switch (team) {
			case "p1":
				FindObjectOfType<FirstPlayer> ().addMoney (20);
				break;
			case "p2":
				FindObjectOfType<SecondPlayer> ().addMoney (20);
				break;
			default:
				Debug.Log ("I do exist but i dont have friends yet");
				break;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		base.OnTriggerEnter2D(collider);
	}

	public void OnTriggerExit2D(Collider2D collider){
		base.OnTriggerExit2D(collider);
	}




}
