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


	public PlayerState playerState = PlayerState.Ingame;
	public Camera cam;
	protected string playerName;
	public int money = 90;
    public int moneyGeneration = 0;

    [SerializeField] protected AudioClip[] sounds;

    protected AudioSource audioS;

	protected Building currentlyBuilding = null;
	protected Casting currentlyCasting = null;
	public float cursorSpeed = 8;
	public Casting bomb;
//	public Text PlayerShop;
	 float timerIncome = 3.0f;
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
        audioS = GetComponent<AudioSource>();

    }

	public void addMoney(int amount){
        if(amount < 0)
        {
            audioS.clip = sounds[0];
            audioS.Play();
        }   
        money += amount;
	}

	// Update is called once per frame
	public void Update () {
		timerIncome -= Time.deltaTime;
		if (timerIncome <= 0) {
			timerIncome = 1.0f;
			addMoney (moneyGeneration);
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
     	   PlayerGoldUI.text = money.ToString();

    }


}
