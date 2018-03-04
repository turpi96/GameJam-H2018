﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Player : MonoBehaviour {

	public enum PlayerState{
		Building = 1,
		Ingame = 2,
		Shop = 3,
		CastingSpell = 4,
		Arrow = 5};


	protected PlayerState playerState = PlayerState.Ingame;
	public Camera cam;
	protected string playerName;
	public int myMoney = 90;

	public int money {
		get{

			return myMoney + moneyToAdd;

		}
		set{
			myMoney = value;


		}


	}
	protected Building currentlyBuilding = null;
	protected Casting currentlyCasting = null;
	public float cursorSpeed = 8;
	public Casting bomb;
//	public Text PlayerShop;
	 float timerIncome = 3.0f;
	int moneyToAdd = 0;
    public GameObject UnitPlayerShop;
    public GameObject TurretPlayerShop;
    public GameObject SpellPlayerShop;
	protected Path pathToFollow;
    public Text PlayerGoldUI;
	public Transform posArrow;

    public int currentSlot = 0;
    public GameObject chooseItem;
    public GameObject[] UnitSlotTable;
    public GameObject[] TurretSlotTable;
    public GameObject[] SpellSlotTable;


    // Use this for initialization
    public void Start () {
		cam = FindObjectOfType<Camera> ();
		pathToFollow = FindObjectOfType<Path> ();
		//openMenu = GetComponent<AudioClip>;
    }

	public void addMoney(int amount){
		moneyToAdd += amount;

	}

	// Update is called once per frame
	public void Update () {
		timerIncome -= Time.deltaTime;
		if (timerIncome <= 0) {
			timerIncome = 3.0f;
			addMoney (20);
		}
		if (moneyToAdd > 50) {
			moneyToAdd -= 25;
			myMoney+= 25;
		} else if (moneyToAdd > 0) {

			moneyToAdd--;
			myMoney++;
		}
        else if (moneyToAdd < -100)
        {
			moneyToAdd+= 25;
			myMoney-= 25;
        }
        else if (moneyToAdd < 00)
        {
            moneyToAdd++;
			myMoney--;
        }

        checkInput ();
		checkPosition ();
        UpdateMoney();
    }

	public void changeState(PlayerState newState){
		playerState = newState;
	}

	public abstract void checkInput();
	public abstract void checkPosition();

    public void UpdateMoney()
    {

//        PlayerGoldUI.text = money.ToString();

		if(PlayerGoldUI != null)
     	   PlayerGoldUI.text = myMoney.ToString();

    }


}
