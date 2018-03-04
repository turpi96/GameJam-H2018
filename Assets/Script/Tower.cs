using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour, CanBeHurt, HasTeam, HasHealth {


	public float health;
	private float maxHealth;
    public string team;

	// Use this for initialization
	void Start () {
        maxHealth = health;
	}

	// Update is called once per frame
	void Update () {

	}
	public float getHealth(){
		return health;
	}
	public float getMaxHealth(){
		return maxHealth;
	}
	public string getTeam(){
		return team;
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
            SceneManager.LoadScene("WinP2");
           
        }
        else if(team == "p2")
        {
            //SCENE 2
            SceneManager.LoadScene("WinP1");
        }
    }
}
