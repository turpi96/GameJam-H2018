using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, CanBeHurt {
    
    public int health;
    public int attack;
    public int attackDelay;
    public int defense;
    public int cost;
    public int value;

    private bool isAttacking;
    private float timeLeft = 0;
    private Collider2D target;

	// Use this for initialization
	void Start () {
        timeLeft = attackDelay;
        target = null;
	}
	
	// Update is called once per frame
	void Update () {
        if(target != null)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                timeLeft = attackDelay;
                target.GetComponent<CanBeHurt>().Hurt(attack);
            }
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.tag != collision.tag)
        {
            isAttacking = true;
            if (collision.GetComponent<CanBeHurt>() != null)
            {
                target = collision;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(this.gameObject.tag != collision.tag)
        {
            isAttacking = false;
            target = null;
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
