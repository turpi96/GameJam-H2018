using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, CanBeHurt {
    
    public int health;
    public int attack;
    public int defense;
    public int cost;
    public int value;

    private bool isAttacking;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public  void Hurt(int amount)
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
        //GIVE VALUE ($) TO THE OTHER PLAYER
        Destroy(this.gameObject);
    }

    void Attack()
    {
        
        //PLAY ANIMATION
        //PLAY SOUND
    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.gameObject.tag != collision.tag)
        {
            isAttacking = true;
			if(collision.GetComponent<CanBeHurt>() != null)
				collision.GetComponent<CanBeHurt> ().Hurt (attack);
          
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(this.gameObject.tag != collision.tag)
        {
            isAttacking = false;
        }
    }

    public bool GetIsAttacking()
    {
        if (isAttacking)
            return true;
        else
            return false;
    }
}
