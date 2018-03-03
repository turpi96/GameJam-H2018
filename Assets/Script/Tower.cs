using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour, CanBeHurt {


    public int health;
    public string team;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

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
        if(team == "p1")
        {
            //SCENE 1
            SceneManager.LoadScene("WinP1");
           
        }
        else if(team == "p2")
        {
            //SCENE 2
            SceneManager.LoadScene("WinP2");
        }
    }
}
