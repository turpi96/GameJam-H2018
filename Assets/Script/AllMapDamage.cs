using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllMapDamage : MonoBehaviour {

    public string team;
    public int damage;
    public int cost;

    private GameObject[] units;
    private GameObject[] turrets;

	// Use this for initialization
	void Start () {

        if (units == null)
        {
            units = GameObject.FindGameObjectsWithTag("Unit");
        }
        if (turrets == null)
        {
            turrets = GameObject.FindGameObjectsWithTag("Turret");
        }


        foreach (GameObject turret in turrets)
        {
            if(turret.GetComponent<HasTeam>().getTeam() != team)
            {
                turret.GetComponent<CanBeHurt>().Hurt(damage);
            }
        }

        foreach (GameObject unit in units)
        {
            if (unit.GetComponent<HasTeam>().getTeam() != team)
            {
                unit.GetComponent<CanBeHurt>().Hurt(damage);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
