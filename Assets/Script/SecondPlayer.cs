﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlayer : Player {

     public GameObject[] workingShop;


	public Building dumbTower;

	public Unit dumbUnit;
	public Transform spawnPoint;
	// Use this for initialization
	new public void Start () {
		base.Start ();
        //UnitPlayerShop.SetActive(false);
    }

    // Update is called once per frame
    new public void Update () {
		base.Update ();

		if (Input.GetKeyDown (KeyCode.P) && playerState == PlayerState.Ingame) {
			changeState (PlayerState.Building);
			currentlyBuilding = Instantiate (dumbTower, new Vector3(transform.position.x,transform.position.y,1),transform.rotation);
			gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			currentlyBuilding.changeState (Building.BuildingState.inConstruction);
			currentlyBuilding.team = "p2";
		} else if (Input.GetKeyDown (KeyCode.P) && playerState == PlayerState.Building && currentlyBuilding.canBuild) {
			changeState (PlayerState.Ingame);
			currentlyBuilding.changeState (Building.BuildingState.inGame);
			currentlyBuilding = null;
			gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		}

		if (Input.GetKeyDown (KeyCode.K) && playerState == PlayerState.Ingame) { 
			Unit unit = Instantiate (dumbUnit, spawnPoint.position, spawnPoint.rotation);
			unit.GetComponent<MoveOnPath> ().PathToFollow = pathToFollow;
		}
	}

	public override void checkInput(){
        checkShopInput();

        float x = 0;
		float y = 0;
		if (Mathf.Abs (Input.GetAxis ("Player2_Horizontal")) > 0.2f)
			x = Input.GetAxis ("Player2_Horizontal");
		if (Mathf.Abs (Input.GetAxis ("Player2_Vertical")) > 0.2f)
			y = Input.GetAxis ("Player2_Vertical");
		switch(playerState){
			case PlayerState.Ingame:
				transform.Translate (new Vector2 (x, y).normalized * cursorSpeed * Time.deltaTime);
				break;
			case PlayerState.Building:
				if (currentlyBuilding != null) {
					transform.Translate (new Vector2 (x, y).normalized * cursorSpeed * Time.deltaTime);
					currentlyBuilding.transform.position = transform.position;
				} else
					Debug.Log ("NO BUILDING FOUND");
				break;
            case PlayerState.Shop:
                if(Input.GetButtonDown("Player2_Up") )
                {
                    workingShop[currentSlot].GetComponent<unitButtonScript>().disableOutline();


                    if (Input.GetAxis("Player2_Up") > 0)
                        currentSlot--;
                    else if (Input.GetAxis("Player2_Up") < 0)
                        currentSlot++;
                    if (currentSlot < 0)
                        currentSlot = 2;
                    if (currentSlot > 2)
                        currentSlot = 0;

                    workingShop[currentSlot].GetComponent<unitButtonScript>().enableOutline();
                }

                break;

			}
	}

	public override void checkPosition(){
		Vector2 halfSize;
		switch (playerState) {
		case PlayerState.Building:
			halfSize.x = currentlyBuilding.GetComponent<SpriteRenderer> ().bounds.size.x * 10.8f;
			halfSize.y = currentlyBuilding.GetComponent<SpriteRenderer> ().bounds.size.y * 10.8f;
			break;
		default:
			halfSize.x = GetComponent<SpriteRenderer> ().bounds.size.x * 10.8f;
			halfSize.y = GetComponent<SpriteRenderer> ().bounds.size.y * 10.8f;
			break;
		}
		//GetComponent<SpriteRenderer> ().sprite.rect.x;
		Vector3 pos = cam.WorldToScreenPoint (transform.position);
		if (pos.x - halfSize.x < cam.pixelWidth / 2.0f) {
			pos.x = cam.pixelWidth / 2.0f + halfSize.x;

		}
		if (pos.x + halfSize.x > cam.pixelWidth) {
			pos.x = cam.pixelWidth - halfSize.x;
		}
		if (pos.y + halfSize.y > cam.pixelHeight) {
			pos.y = cam.pixelHeight - halfSize.y;
		}
		if (pos.y < 0 + halfSize.y) {
			pos.y = 0 + halfSize.y;
		}
		if (pos.x < 0 + halfSize.x) {
			pos.x = 0 + halfSize.x;
		}
		transform.position = cam.ScreenToWorldPoint (pos);
	}

	public override void spawnBomb(){
		if (Input.GetMouseButtonDown (0)) {
			Vector3 mousePos = Input.mousePosition;
			Vector3 posCam = cam.ScreenToWorldPoint (mousePos);
			posCam.z = 0;
			GameObject g = Instantiate (bomb, posCam, Quaternion.identity) as Casting;
			g.tag = transform.tag;
		}
	}


	public override void spawnArrow(){
		/*if (Input.GetMouseButtonDown (1)) {
			Vector3 mousePos = Input.mousePosition;
			Vector3 posCam = cam.ScreenToWorldPoint (mousePos);
			posCam.z = 0;
			Instantiate (bomb,posCam , Quaternion.identity,transform);
		}*/
	}


<<<<<<< HEAD
    private void checkShopInput()
    {
        /****************************************UNITS********************************************/
		if (UnitPlayerShop.activeSelf == true &&
            TowerPlayerShop.activeSelf == false && 
            Input.GetButtonDown("Player2_Left"))
        {
            workingShop[currentSlot].GetComponent<unitButtonScript>().disableOutline();

            changeState(PlayerState.Ingame);
            UnitPlayerShop.SetActive(false);
        }
        else if (UnitPlayerShop.activeSelf == false &&
            TowerPlayerShop.activeSelf == false && 
            Input.GetButtonDown("Player2_Left"))
        {
            currentSlot = 0;
            copyArray(UnitSlotTable);
            workingShop[currentSlot].GetComponent<unitButtonScript>().enableOutline();
=======

>>>>>>> 0627c9afa2487c075c138bdbf75b21bb05496668

    private void checkShopInput()
    {
		if (UnitPlayerShop != null) {
			if (UnitPlayerShop.activeSelf == true && Input.GetButtonDown ("Player2_Left")) {
				workingShop [currentSlot].GetComponent<unitButtonScript> ().disableOutline ();

				changeState (PlayerState.Ingame);
				UnitPlayerShop.SetActive (false);
			} else if (UnitPlayerShop.activeSelf == false && Input.GetButtonDown ("Player2_Left")) {
				currentSlot = 0;
				copyArray (UnitSlotTable);
				workingShop [currentSlot].GetComponent<unitButtonScript> ().enableOutline ();

				changeState (PlayerState.Shop);
				UnitPlayerShop.SetActive (true);
			}

<<<<<<< HEAD
       if (Input.GetButtonDown("Player2_Accept") && 
            UnitPlayerShop.activeSelf == true &&
            workingShop[currentSlot].GetComponent<unitButtonScript>().interactable == true)
        {
            setChoosenItem();
        }
       /***************************************************************************************************/





=======
			if (Input.GetButtonDown ("Player2_Accept") &&
			        workingShop [currentSlot].GetComponent<unitButtonScript> ().interactable == true) {
				setChoosenItem ();
			}
		}
>>>>>>> 0627c9afa2487c075c138bdbf75b21bb05496668
    }

    private void copyArray(GameObject[] tempArray)
    {
        for(int i = 0; i < tempArray.Length; i++)
        {
            workingShop[i] = tempArray[i];       
        }

    }

    public void setChoosenItem()
    {
        chooseItem = workingShop[currentSlot];
        Debug.Log("I, IS A GENIUS!!!!");
    }

    public void emptyChoosenItem()
    {
        chooseItem = null;
    }
}
