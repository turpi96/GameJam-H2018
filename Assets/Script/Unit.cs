using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, CanBeHurt {

    public string team;
    public int health;
    public int attack;
    public int attackDelay;
    public bool isRange;
    public int defense;
    public int cost;
    public int value;

    private bool isAttacking;
    private float timeLeft = 0;
    private List<Collider2D> targetList;
    private Animator animator;
    private bool isUnit = false;
    private bool isTurret = false;
    private bool isTower = false;


	// Use this for initialization
	void Start () {
        timeLeft = attackDelay;
        targetList = new List<Collider2D>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if(targetList.Count >= 1)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                timeLeft = attackDelay;
                targetList[0].GetComponent<CanBeHurt>().Hurt(attack);
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
        if (collision.GetComponent<Unit>() != null)
            isUnit = true;
        if (collision.GetComponent<Turret>() != null)
            isTurret = true;
        if (collision.GetComponent<Tower>() != null)
            isTower = true;

        if ((isUnit && team != collision.GetComponent<Unit>().team) || (isTurret && team != collision.GetComponent<Turret>().team) || (isTower && team != collision.GetComponent<Tower>().team)) {
			if ((!isRange && collision.tag == "Unit") || (isRange && (collision.tag == "Unit" || collision.tag == "Turret")) || collision.tag == "Tower") {
				if (collision.GetComponent<CanBeHurt> () != null) {
					//if (Vector3.Distance(collision.transform.position, this.transform.position) <= range)
					isAttacking = true;
                    animator.SetBool("IsAttacking", true);
					targetList.Add (collision);
				}
			}
		}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((isUnit && team != collision.GetComponent<Unit>().team) || (isTurret && team != collision.GetComponent<Turret>().team) || (isTower && team != collision.GetComponent<Tower>().team))
        {
            if ((!isRange && collision.tag == "Unit") || (isRange && (collision.tag == "Unit" || collision.tag == "Turret"))) {
					targetList.Remove (collision);
                if (collision.GetComponent<Unit>() != null)
                    isUnit = false;
                if (collision.GetComponent<Turret>() != null)
                    isTurret = false;
                if (collision.GetComponent<Tower>() != null)
                    isTower = false;
                if (targetList.Count == 0) {
						isAttacking = false;
                        animator.SetBool("IsAttacking", false);
                    }
                
			}
		}
    }

    public bool GetIsAttacking()
    {
        if (isAttacking)
            return true;
        else
            return false;
    }

    public void StartFreeze(float freezePercentage)
    {
        GetComponent<MoveOnPath>().MultiplySpeed(freezePercentage);
        animator.SetFloat("FreezePercentage", 0.5f);
    }

    public void StopFreeze()
    {
        GetComponent<MoveOnPath>().ResetSpeed();
        animator.SetFloat("FreezePercentage", 1.0f);
    }
}
