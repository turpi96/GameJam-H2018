using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, CanBeHurt,HasTeam, HasHealth {

    public string team;
    public float health;
    public int attack;
    public float attackDelay;
    public bool isRange;
    public bool isAttackingTurret;
    public int defense;
    public int cost;
    public int value;
    public GameObject bullet;

    private int freezeTurretsCount = 0;

    private float currentDelay;
    private bool isAttacking;
    private float timeLeft = 0;
    private List<Collider2D> targetList;
    private Animator animator;
    private float maxHealth;
    private GameObject shootingObject;



    // Use this for initialization
    void Start () {
        maxHealth = health;
        currentDelay = attackDelay;
        timeLeft = currentDelay;
        targetList = new List<Collider2D>();
        animator = GetComponent<Animator>();
	}
	public string getTeam(){
		return team;
	}
	public float getHealth(){
		return health;
	}
	public float getMaxHealth(){
		return maxHealth;
	}
	// Update is called once per frame
	void Update () {
        if(targetList.Count >= 1)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                if(bullet != null)
                {
                    shootingObject = targetList[0].gameObject;
                    Vector3 bulletPos;
                    Transform spawn = transform.Find("SpawnBullet");
                    if (spawn != null)
                    {
                        bulletPos = spawn.position;
                    }
                    else
                    {
                        bulletPos = transform.position;
                    }
                    GameObject go = Instantiate(bullet, bulletPos, Quaternion.identity) as GameObject;
                    go.GetComponent<Bullet>().Initialise(shootingObject);
                }
                timeLeft = currentDelay;
                if(!isRange)
                    targetList[0].GetComponent<CanBeHurt>().Hurt(attack);
            }
        }
    }


    public  void Hurt(int amount)
    {
        health = health - (amount * (1 - defense/100));

        GetComponent<SpriteRenderer>().color = new Color(0.604f,0.13f,0.13f);
        StartCoroutine(TimerWhiteColor());

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
        if (team == "p1")
        {
            if(FindObjectOfType<SecondPlayer>() != null)
                FindObjectOfType<SecondPlayer>().addMoney(value);
        }
        else
        {
            if (FindObjectOfType<FirstPlayer>() != null)
                FindObjectOfType<FirstPlayer>().addMoney(value);
        }
        Destroy(this.gameObject);
    }

    void Attack()
    {
        
        //PLAY ANIMATION
        //PLAY SOUND
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "RangeCollider")
        {
            if (collision.GetComponent<HasTeam>() != null)
            {
                if (team != collision.GetComponent<HasTeam>().getTeam())
                {
					
                    if ((!isRange && !isAttackingTurret && collision.tag == "Unit") || (isRange && isAttackingTurret && (collision.tag == "Turret") || collision.tag =="Unit") || (isRange && !isAttackingTurret && collision.tag == "Unit") || collision.tag == "Tower")
                    {
						if (isRange && collision.tag == "Turret") {
							if (collision.GetComponent<Building> ().state == Building.BuildingState.inGame) {
								isAttacking = true;
								animator.SetBool ("IsAttacking", true);
								targetList.Add (collision);
							}

						} else {
							if (collision.GetComponent<CanBeHurt> () != null) {
								//if (Vector3.Distance(collision.transform.position, this.transform.position) <= range)
								isAttacking = true;
								animator.SetBool ("IsAttacking", true);
								targetList.Add (collision);
							}
						}
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
		if (collision.GetComponent<HasTeam> () != null) {
			if (team != collision.GetComponent<HasTeam>().getTeam()) {
                if ((!isRange && !isAttackingTurret && collision.tag == "Unit") || (isRange && isAttackingTurret && (collision.tag == "Turret") || collision.tag == "Unit") || (isRange && !isAttackingTurret && collision.tag == "Unit") || collision.tag == "Tower")
                {
					if (isRange && collision.tag == "Turret") {
						if (collision.GetComponent<Building> ().state == Building.BuildingState.inGame) {
							targetList.Remove (collision);
				
						}
					}
					else
                   		 targetList.Remove (collision);
					if (targetList.Count == 0) {
						isAttacking = false;
						animator.SetBool ("IsAttacking", false);
					}
                
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
        freezeTurretsCount++;
        if (freezeTurretsCount == 1)
        {
            GetComponent<MoveOnPath>().MultiplySpeed(freezePercentage);
            currentDelay = (currentDelay * (2 - freezePercentage));
            animator.SetFloat("FreezePercentage", 0.5f);
        }
    }

    public void StopFreeze()
    {
        freezeTurretsCount--;
        if (freezeTurretsCount == 0)
        {
            GetComponent<MoveOnPath>().ResetSpeed();
            currentDelay = attackDelay;
            animator.SetFloat("FreezePercentage", 1.0f);
        }
    }

    IEnumerator TimerWhiteColor()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
