using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Player : MonoBehaviour {

	public enum PlayerState{
		Building = 1,
		Ingame = 2,
		Shop = 3,
		CastingSpell = 4};


	protected PlayerState playerState = PlayerState.Ingame;
	public Camera cam;
	protected string playerName;
	protected int money = 90;
	protected Building currentlyBuilding = null;
	protected Casting currentlyCasting = null;
	public float cursorSpeed = 5;
	public Casting bomb;
//	public Text PlayerShop;

    public GameObject UnitPlayerShop;
    public GameObject TowerPlayerShop;
    public GameObject SpellPlayerShop;
	protected Path pathToFollow;
    public Text PlayerGoldUI;

    public int currentSlot = 0;
    public GameObject chooseItem;
    public GameObject[] UnitSlotTable;
    public GameObject[] TowerSlotTable;
    public GameObject[] SpellSlotTable;


    // Use this for initialization
    public void Start () {
		cam = FindObjectOfType<Camera> ();
		pathToFollow = FindObjectOfType<Path> ();
    }
	
	// Update is called once per frame
	public void Update () {
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
