using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public string unitName;
    public int health;
    public int attack;
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

    void Die()
    {
        //MAYBE PLAY AN ANIMATION
        //MAYBE PLAY A SOUND
        Destroy(this);
    }

    void Attack()
    {
        //PLAY ANIMATION
        //PLAY SOUND
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "unit")
        {
            collision.gameObject.GetComponent<Unit>().Hurt(attack);
        }
        if(collision.tag == "tower")
        {
            collision.gameObject.GetComponent<Tower>().Hurt(attack);
        }
    }
}
