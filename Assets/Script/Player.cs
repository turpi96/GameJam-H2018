using System.Collections;
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
	protected string playerName;
	protected int money = 100;
	protected Building currentlyBuilding = null;
	public float cursorSpeed = 5;
	public GameObject bomb;
	public Material matBomb;
//	public Text PlayerShop;
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
        //PlayerGoldUI.text = money.ToString();
    }


}
