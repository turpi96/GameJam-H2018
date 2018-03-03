﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Player : MonoBehaviour {

	public enum PlayerState{
		Building = 1,
		Ingame = 2,
		Shop = 3};


	protected PlayerState playerState = PlayerState.Ingame;
	public Camera cam;
<<<<<<< HEAD
	protected int money = 100;
	protected Tower currentlyBuilding = null;
=======
	protected int money;
	protected Building currentlyBuilding = null;
>>>>>>> 57ac45b4cab24e9eb70fd8c2620b9e4642bb2e56
	public float cursorSpeed = 5;
	public GameObject bomb;
	public Material matBomb;

    public GameObject UnitPlayerShop;
    public GameObject TowerPlayerShop;
    public GameObject SpellPlayerShop;
    public Text PlayerGoldUI;

	// Use this for initialization
	public void Start () {
		cam = FindObjectOfType<Camera> ();
    }
	
	// Update is called once per frame
	public void Update () {
		checkInput ();
		checkPosition ();
		spawnBomb ();
        UpdateMoney();
    }

	public void changeState(PlayerState newState){
		playerState = newState;
	}

	public abstract void checkInput();
	public abstract void checkPosition();
	public abstract void spawnBomb();

    public void UpdateMoney()
    {
        PlayerGoldUI.text = money.ToString();
    }


}
