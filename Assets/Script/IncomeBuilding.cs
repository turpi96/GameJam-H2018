using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomeBuilding : Building {
	float timerIncome = 3.0f;
	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
		timerIncome -= Time.deltaTime;
		if (timerIncome < 0) {
			timerIncome = 3.0f;
			switch (team) {
			case "p1":
				FindObjectOfType<FirstPlayer> ().addMoney (25);
				break;
			case "p2":
				FindObjectOfType<SecondPlayer> ().addMoney (25);
				break;
			default:
				Debug.Log ("I do exist but i dont have friends yet");
				break;
			}
		}
	}

	new void OnTriggerEnter2D(Collider2D collider){
		base.OnTriggerEnter2D(collider);
	}

	new public void OnTriggerExit2D(Collider2D collider){
		base.OnTriggerExit2D(collider);
	}




}
