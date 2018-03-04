using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllMapDamage : MonoBehaviour {

    public string team;
    public int damage;
    public int cost;

    public int minSpawn;
    public int maxSpawn;

    public GameObject ThingtoSpawn;

    private GameObject[] units;
    private GameObject[] turrets;

	// Use this for initialization
	void Start ()
    {
        int nbToSpawn = Random.Range(minSpawn, maxSpawn);

        for(int i = 0; i < nbToSpawn; i++)
        {
            Vector3 position = new Vector3(Random.Range(-18.0f,18.0f),Random.Range(-9.3f,9.5f),-15);
            Instantiate(ThingtoSpawn, position, Quaternion.identity);
        }

        StartCoroutine(Explode());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(1.0f);

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
            if (turret.GetComponent<HasTeam>().getTeam() != team)
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

        GetComponent<AudioSource>().Play();

        Destroy(gameObject,0.8f);
    }
}
