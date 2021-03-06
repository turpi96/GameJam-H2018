﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Building {

   
    private List<GameObject> shootingList;
    private bool isShooting = false;
    private GameObject shootingObject;
    private float timeLeft;
	public bool hasToRotate = true;

  
    public float shootingDelay = 1.0f;
    public GameObject bulletToShoot;

	// Use this for initialization
	void Start () {
		base.Start ();
        shootingList = new List<GameObject>();
        timeLeft = 0.0f;
    }


	// Update is called once per frame
	new void Update () {
		base.Update ();
		if (state == BuildingState.inGame) {
			if (isShooting) {
				timeLeft -= Time.deltaTime;

				if (timeLeft <= 0) {
					if (shootingObject != null) {
                        Vector3 diff = shootingObject.transform.position - transform.position;
                        diff.Normalize();
						if (hasToRotate) {
							float rot_z = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
							transform.rotation = Quaternion.Euler (0f, 0f, rot_z - 90);

						}
                        Vector3 bulletPos;
                        Transform spawn = transform.Find("BulletSpawn");
                        if (spawn != null)
                        {
                            bulletPos = spawn.position;
                        }
                        else
                        {
                            bulletPos = transform.position;
                        }

                        GetComponent<AudioSource>().Play();

                        GameObject go = Instantiate (bulletToShoot, bulletPos, Quaternion.identity) as GameObject;
						go.GetComponent<Bullet> ().Initialise (shootingObject);
						timeLeft = shootingDelay;
					} else {
						shootingList.Remove (shootingObject);
						if (shootingList.Count > 0) {
							shootingObject = shootingList [0];
						} else {
							isShooting = false;
						}
					}
				}     
			}
		}
    }

    new void OnTriggerEnter2D(Collider2D other)
    { 
		base.OnTriggerEnter2D (other);
		if (state == BuildingState.inGame && other.GetComponent<Unit>())
        {
            if (team != other.GetComponent<HasTeam>().getTeam())
            {
                shootingList.Add(other.gameObject);
                isShooting = true;
                if (shootingList.Count == 1)
                {
                    shootingObject = shootingList[0];
                }
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
                shootingList.Remove(other.gameObject);
                if (shootingList.Count > 0)
                {
                    shootingObject = shootingList[0];
                    isShooting = true;
                }
                else
                    isShooting = false;
            }
        }
    }
}
