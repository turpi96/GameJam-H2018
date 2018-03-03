﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public enum PlayerState{
		Building = 1,
		Ingame = 2,
		Shop = 3};


	protected PlayerState playerState = PlayerState.Ingame;
	protected int health;
	protected int money;
	public float cursorSpeed = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeState(PlayerState newState){
		playerState = newState;
	}

}
