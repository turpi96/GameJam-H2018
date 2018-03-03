using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public int health;
    public int attack;
    public int walkspeed;
    public int defense;
    public int cost;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Hurt(int amount)
    {
        health = health - (amount - defense);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //MAYBE PLAY AN ANIMATION
        //MAYBE PLAY A SOUND
        Destroy(this);
    }

    void Attack()
    {

    }
}
