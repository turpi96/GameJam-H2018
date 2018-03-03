using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private List<GameObject> shootingList;
    private bool isShooting = false;
    private GameObject shootingOject;
    private float timeLeft;

    public float shootingDelay = 1.0f;
    public GameObject bulletToShoot;

	// Use this for initialization
	void Start () {
        shootingList = new List<GameObject>();
        timeLeft = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
		if(isShooting)
        {
            timeLeft -= Time.deltaTime;

            if(timeLeft <= 0)
            {
                if (shootingOject != null)
                {
                    GameObject go = Instantiate(bulletToShoot,transform.position,transform.rotation) as GameObject;
                    go.GetComponent<Bullet>().Initialise(shootingOject);
                }
                
                timeLeft = shootingDelay;
            }

            
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Unit>())
        {
            shootingList.Add(other.gameObject);
            isShooting = true;
            if (shootingList.Count == 1)
            {
                shootingOject = shootingList[0];
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Unit>())
        {
            shootingList.Remove(other.gameObject);
            if (shootingList.Count > 0)
            {
                shootingOject = shootingList[0];
                isShooting = true;
            }
            else
                isShooting = false;
        }
    }
}
