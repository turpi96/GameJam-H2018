using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFreeze : Building {

    public float freezePercentage = 0.7f;

	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();	
	}

    new void OnTriggerEnter2D(Collider2D other)
    {
		base.OnTriggerEnter2D (other);
        if (state == BuildingState.inGame && other.GetComponent<Unit>())
        {
            if (team != other.GetComponent<HasTeam>().getTeam())
            {
                other.GetComponent<Unit>().StartFreeze(freezePercentage);
            }
        }
    }

    new void OnTriggerExit2D(Collider2D other)
    {
		base.OnTriggerExit2D (other);
		if (state == BuildingState.inGame && other.GetComponent<Unit>())
        {
            if (team != other.GetComponent<HasTeam>().getTeam())
            {
                other.GetComponent<Unit>().StopFreeze();
            }
        }
    }
}
