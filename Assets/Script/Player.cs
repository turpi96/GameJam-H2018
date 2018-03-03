using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour {

	public enum PlayerState{
		Building = 1,
		Ingame = 2,
		Shop = 3};


	protected PlayerState playerState = PlayerState.Ingame;
	public Camera cam;
	protected int money;
	protected Tower currentlyBuilding = null;
	public float cursorSpeed = 5;
	public GameObject bomb;
	public Material matBomb;

    public GameObject PlayerShop;
    public GameObject PlayerGold;
	// Use this for initialization
	public void Start () {
		cam = FindObjectOfType<Camera> ();
	}
	
	// Update is called once per frame
	public void Update () {
		checkInput ();
		checkPosition ();
		spawnBomb ();

	}

	public void changeState(PlayerState newState){
		playerState = newState;
	}

	public abstract void checkInput();
	public abstract void checkPosition();
	public abstract void spawnBomb();


}
