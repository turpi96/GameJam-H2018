using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFreeze : Building {

    public float freezePercentage = 0.7f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
		
        if (state == BuildingState.inGame && other.GetComponent<Unit>())
        {
            other.GetComponent<Unit>().StartFreeze(freezePercentage);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
		if (state == BuildingState.inGame && other.GetComponent<Unit>())
        {
            other.GetComponent<Unit>().StopFreeze();
        }
    }
}
