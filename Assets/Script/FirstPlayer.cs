using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayer : Player {
    public GameObject[] workingShop;
    public Building dumbTower;
	public Unit dumbUnit;
	public float timerShop = 0.2f;
	public Transform spawnPoint;
	// Use this for initialization
	new public void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	new public void Update () {
		base.Update ();
	}

	public  override void checkInput(){

		if (Input.GetButtonDown ("Player1_A") && playerState == PlayerState.Building && currentlyBuilding.canBuild) {
			changeState (PlayerState.Ingame);
			currentlyBuilding.changeState (Building.BuildingState.inGame);
            addMoney(-currentlyBuilding.cost);
            currentlyBuilding = null;
			gameObject.GetComponent<SpriteRenderer> ().enabled = true;
        }

		if (Input.GetButtonDown ("Player1_A") && playerState == PlayerState.CastingSpell) {
			changeState (PlayerState.Ingame);
			currentlyCasting.changeState (Casting.CastingState.inGame);
            addMoney(-currentlyCasting.GetComponent<Bomb>().cost);
            currentlyCasting = null;
			gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		}
		if (playerState == PlayerState.Ingame || playerState == PlayerState.Shop) {
			checkShopInputTurret ();
			checkShopInputUnits ();

			checkShopInputSpell ();
		}
		if(Input.GetButtonDown ("Player1_B") && playerState == PlayerState.Building){
			Destroy (currentlyBuilding.gameObject);
			currentlyBuilding = null;
			changeState (PlayerState.Ingame);
			gameObject.GetComponent<SpriteRenderer> ().enabled = true;

		}
        float x = 0;
		float y = 0;
		if (Mathf.Abs (Input.GetAxis ("Player1_Horizontal")) > 0.2f)
			x = Input.GetAxis ("Player1_Horizontal");
		if (Mathf.Abs (Input.GetAxis ("Player1_Vertical")) > 0.2f)
			y = Input.GetAxis ("Player1_Vertical");

		switch(playerState){
			case PlayerState.Ingame:
				transform.Translate (new Vector2 (x, y).normalized * cursorSpeed * Time.deltaTime);
				break;
			case PlayerState.Building:
				if (currentlyBuilding != null) {
					transform.Translate (new Vector2 (x, y).normalized * cursorSpeed * Time.deltaTime);
					currentlyBuilding.transform.position = transform.position;
				}
				else
					Debug.Log ("NO BUILDING FOUND");
				break;

		case PlayerState.Shop:
				timerShop -= Time.deltaTime;
				if (Input.GetAxisRaw ("Player1_Up") != 0) {
				if (timerShop <= 0) {
					timerShop = 0.2f;
					workingShop [currentSlot].GetComponent<outlineScript> ().disableOutline ();


					if (Input.GetAxis ("Player1_Up") < 0)
						currentSlot--;
					else if (Input.GetAxis ("Player1_Up") > 0)
						currentSlot++;
					if (currentSlot < 0)
						currentSlot = 2;
					if (currentSlot > 2)
						currentSlot = 0;

					workingShop [currentSlot].GetComponent<outlineScript> ().enableOutline ();
				}
				}
			

                break;


            case PlayerState.CastingSpell:
			    if (currentlyCasting != null) {
				    transform.Translate (new Vector2 (x, y).normalized * cursorSpeed * Time.deltaTime);
				    currentlyCasting.transform.position = transform.position;
			    }
			    break;
			 default:
				transform.Translate (new Vector2 (x, y).normalized * cursorSpeed * Time.deltaTime);
				break;
		}

	}

	public override void checkPosition(){
			Camera cam = FindObjectOfType<Camera> ();

			Vector2 halfSize;
			switch (playerState) {

			case PlayerState.Building:
				halfSize.x = currentlyBuilding.GetComponent<SpriteRenderer> ().bounds.size.x * 10.8f;
				halfSize.y = currentlyBuilding.GetComponent<SpriteRenderer> ().bounds.size.y * 10.8f;
				break;
			case PlayerState.CastingSpell:
					halfSize.x = currentlyCasting.GetComponent<SpriteRenderer> ().bounds.size.x * 10.8f;
					halfSize.y = currentlyCasting.GetComponent<SpriteRenderer> ().bounds.size.y * 10.8f;
					break;
			default:
				halfSize.x = GetComponent<SpriteRenderer> ().bounds.size.x ;
				halfSize.y = GetComponent<SpriteRenderer> ().bounds.size.y;
				break;
			}
		Vector3 pos = cam.WorldToScreenPoint (transform.position);

		if (playerState != PlayerState.CastingSpell) {
			if (pos.x + halfSize.x > cam.pixelWidth / 2.0f) {
				pos.x = cam.pixelWidth / 2.0f - halfSize.x;
			} 
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

    private void checkShopInputUnits()
    {
        if (UnitPlayerShop != null)
        {
			if (UnitPlayerShop.activeSelf == true &&
			             TurretPlayerShop.activeSelf == false &&
			             SpellPlayerShop.activeSelf == false &&
			             Input.GetButtonDown ("Player1_X")) {
				workingShop [currentSlot].GetComponent<outlineScript> ().disableOutline ();

				changeState (PlayerState.Ingame);
				UnitPlayerShop.SetActive (false);
			} else if (UnitPlayerShop.activeSelf == false &&
			                  TurretPlayerShop.activeSelf == false &&
			                  SpellPlayerShop.activeSelf == false &&
			                  Input.GetButtonDown ("Player1_X")) {
				changeState (PlayerState.Shop);
				UnitPlayerShop.SetActive (true);
				currentSlot = 0;
				copyArray (UnitSlotTable);
				workingShop [currentSlot].GetComponent<outlineScript> ().enableOutline ();


			} else if (UnitPlayerShop.activeSelf == true &&
			        Input.GetButtonDown ("Player1_B")) {
				UnitPlayerShop.SetActive (false);
				workingShop [currentSlot].GetComponent<outlineScript> ().disableOutline ();
				changeState (PlayerState.Ingame);

			}


            if (Input.GetButtonDown("Player1_A") &&
                    UnitPlayerShop.activeSelf == true &&
                    workingShop[currentSlot].GetComponent<unitButtonScript>().interactable == true)
            {
                setChoosenItem();
                workingShop[currentSlot].GetComponent<outlineScript>().disableOutline();
                //INSERT UNIT SPAWN FUCNTION HERE****************************************************************
                UnitPlayerShop.SetActive(false);
            }


        }
    }

    private void checkShopInputTurret()
    {
        if (TurretPlayerShop != null)
        {
            if (TurretPlayerShop.activeSelf == true &&
               UnitPlayerShop.activeSelf == false &&
               SpellPlayerShop.activeSelf == false &&
               Input.GetButtonDown("Player1_B"))
            {
                workingShop[currentSlot].GetComponent<outlineScript>().disableOutline();
                changeState(PlayerState.Ingame);
                TurretPlayerShop.SetActive(false);
            }
            else if (TurretPlayerShop.activeSelf == false &&
                    UnitPlayerShop.activeSelf == false &&
                    SpellPlayerShop.activeSelf == false &&
                    Input.GetButtonDown("Player1_B"))
            {
                changeState(PlayerState.Shop);
                TurretPlayerShop.SetActive(true);
                currentSlot = 0;
                copyArray(TurretSlotTable);
                workingShop[currentSlot].GetComponent<outlineScript>().enableOutline();

            }


            if (Input.GetButtonDown("Player1_A") &&
                    TurretPlayerShop.activeSelf == true &&
                    workingShop[currentSlot].GetComponent<turretButtonScript>().interactable == true)
            {
                //changeState(PlayerState.Building);
                setChoosenItem();
                workingShop[currentSlot].GetComponent<outlineScript>().disableOutline();
                TurretPlayerShop.SetActive(false);
            }

        }
    }

    private void checkShopInputSpell()
    {
        if (SpellPlayerShop != null)
        {
            if (SpellPlayerShop.activeSelf == true &&
               UnitPlayerShop.activeSelf == false &&
               TurretPlayerShop.activeSelf == false &&
               Input.GetButtonDown("Player1_Y"))
            {
                workingShop[currentSlot].GetComponent<outlineScript>().disableOutline();
                changeState(PlayerState.Ingame);
                SpellPlayerShop.SetActive(false);
            }
            else if (SpellPlayerShop.activeSelf == false &&
                    TurretPlayerShop.activeSelf == false &&
                    UnitPlayerShop.activeSelf == false &&
					playerState == PlayerState.Ingame &&
                    Input.GetButtonDown("Player1_Y"))
            {
                changeState(PlayerState.Shop);
                SpellPlayerShop.SetActive(true);
                currentSlot = 0;
                copyArray(SpellSlotTable);
                workingShop[currentSlot].GetComponent<outlineScript>().enableOutline();

			}else if (SpellPlayerShop.activeSelf == true &&
				Input.GetButtonDown ("Player1_B")) {
				SpellPlayerShop.SetActive (false);
				workingShop [currentSlot].GetComponent<outlineScript> ().disableOutline ();
				changeState (PlayerState.Ingame);

			}



            if (Input.GetButtonDown("Player1_A") &&
                    SpellPlayerShop.activeSelf == true &&
                    workingShop[currentSlot].GetComponent<spellButtonScript>().interactable == true)
            {
                setChoosenItem();
                workingShop[currentSlot].GetComponent<outlineScript>().disableOutline();
                SpellPlayerShop.SetActive(false);
            }

        }
    }

    private void copyArray(GameObject[] tempArray)
    {
        for (int i = 0; i < tempArray.Length; i++)
        {
            workingShop[i] = tempArray[i];
        }

    }

    public void setChoosenItem()
    {
		
        chooseItem = workingShop[currentSlot];
		if (chooseItem.GetComponent<unitButtonScript> () != null) {
			Unit unit = Instantiate (chooseItem.GetComponent<unitButtonScript> ().myUnit.GetComponent<Unit> (), spawnPoint.position, spawnPoint.rotation);
			unit.GetComponent<MoveOnPath> ().PathToFollow = pathToFollow;
			unit.team = "p1";
            addMoney(-unit.cost);
			changeState (PlayerState.Ingame);
		} else if (chooseItem.GetComponent<turretButtonScript> () != null) {
			changeState (PlayerState.Building);
			currentlyBuilding = Instantiate (chooseItem.GetComponent<turretButtonScript>().myTurret.GetComponent<Building>(), new Vector3(transform.position.x,transform.position.y,1),transform.rotation);
			gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			currentlyBuilding.changeState (Building.BuildingState.inConstruction);
			currentlyBuilding.team = "p1";
        } else if (chooseItem.GetComponent<spellButtonScript> () != null) {

			GameObject g = chooseItem.GetComponent<spellButtonScript> ().mySpell;
			if (g.GetComponent<Bomb> () != null) {
				changeState (PlayerState.CastingSpell);
				currentlyCasting = Instantiate (g.GetComponent<Bomb>(),new Vector3(transform.position.x,transform.position.y,1),transform.rotation);
				gameObject.GetComponent<SpriteRenderer> ().enabled = false;
				currentlyCasting.changeState (Casting.CastingState.isTargeting);
			}
			else if(g.GetComponent<ArrowSpell>() != null){
				Instantiate (g.GetComponent<ArrowSpell> (),posArrow.position,Quaternion.identity);
				changeState (PlayerState.Arrow);
			}

			else if(g.GetComponent<AllMapDamage>() != null){
				Instantiate (g.GetComponent < AllMapDamage> ());
				changeState (PlayerState.Ingame);
                addMoney(-g.GetComponent<AllMapDamage>().cost);
			}
	
		}
    }

    public void emptyChoosenItem()
    {
        chooseItem = null;
    }
}
