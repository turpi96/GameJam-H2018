using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayer : Player {
    public GameObject[] workingShop;
    public Building dumbTower;
	public Unit dumbUnit;
	public Transform spawnPoint;
	// Use this for initialization
	new public void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	new public void Update () {
		base.Update ();
		if (Input.GetKeyDown (KeyCode.O) && playerState == PlayerState.Ingame) {
			changeState (PlayerState.Building);
			currentlyBuilding = Instantiate (dumbTower, new Vector3(transform.position.x,transform.position.y,1),transform.rotation);
			gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			currentlyBuilding.changeState (Building.BuildingState.inConstruction);
			currentlyBuilding.team = "p1";
		} else if (Input.GetKeyDown (KeyCode.O) && playerState == PlayerState.Building && currentlyBuilding.canBuild) {
			changeState (PlayerState.Ingame);
			currentlyBuilding.changeState (Building.BuildingState.inGame);
			currentlyBuilding = null;
			gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		}

		if (Input.GetKeyDown (KeyCode.U) && playerState == PlayerState.Ingame) { 
			changeState (PlayerState.CastingSpell);
			currentlyCasting = Instantiate (bomb, new Vector3(transform.position.x,transform.position.y,1),transform.rotation) as Casting;
			gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			currentlyCasting.changeState(Casting.CastingState.isTargeting);
		} 
		else if (Input.GetKeyDown (KeyCode.U) && playerState == PlayerState.CastingSpell && currentlyCasting.canCast) {
			changeState (PlayerState.Ingame);
			currentlyCasting.changeState (Casting.CastingState.inGame);
			currentlyCasting = null;
			gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		}

		if (Input.GetKeyDown (KeyCode.J) && playerState == PlayerState.Ingame) { 
			Unit unit = Instantiate (dumbUnit, spawnPoint.position, spawnPoint.rotation);
			unit.GetComponent<MoveOnPath> ().PathToFollow = pathToFollow;
		}
	}

	public  override void checkInput(){
        checkShopInputUnits();
        checkShopInputTurret();
        checkShopInputSpell();

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
                if (Input.GetButtonDown("Player1_Up"))
                {
                    workingShop[currentSlot].GetComponent<outlineScript>().disableOutline();


                    if (Input.GetAxis("Player1_Up") > 0)
                        currentSlot--;
                    else if (Input.GetAxis("Player1_Up") < 0)
                        currentSlot++;
                    if (currentSlot < 0)
                        currentSlot = 2;
                    if (currentSlot > 2)
                        currentSlot = 0;

                    workingShop[currentSlot].GetComponent<outlineScript>().enableOutline();
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
               Input.GetButtonDown("Player1_Left"))
            {
                Debug.Log("I'VE BEEN THERE");
                workingShop[currentSlot].GetComponent<outlineScript>().disableOutline();

                changeState(PlayerState.Ingame);
                UnitPlayerShop.SetActive(false);
            }
            else if (UnitPlayerShop.activeSelf == false &&
                    TurretPlayerShop.activeSelf == false &&
                    SpellPlayerShop.activeSelf == false &&
                    Input.GetButtonDown("Player1_Left"))
            {
                changeState(PlayerState.Shop);
                UnitPlayerShop.SetActive(true);
                currentSlot = 0;
                copyArray(UnitSlotTable);
                workingShop[currentSlot].GetComponent<outlineScript>().enableOutline();


            }


            if (Input.GetButtonDown("Player1_Accept") &&
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
               Input.GetButtonDown("Player1_Right"))
            {
                workingShop[currentSlot].GetComponent<outlineScript>().disableOutline();
                changeState(PlayerState.Ingame);
                TurretPlayerShop.SetActive(false);
            }
            else if (TurretPlayerShop.activeSelf == false &&
                    UnitPlayerShop.activeSelf == false &&
                    SpellPlayerShop.activeSelf == false &&
                    Input.GetButtonDown("Player1_Right"))
            {
                changeState(PlayerState.Shop);
                TurretPlayerShop.SetActive(true);
                currentSlot = 0;
                copyArray(TurretSlotTable);
                workingShop[currentSlot].GetComponent<outlineScript>().enableOutline();


            }


            if (Input.GetButtonDown("Player1_Accept") &&
                    TurretPlayerShop.activeSelf == true &&
                    workingShop[currentSlot].GetComponent<turretButtonScript>().interactable == true)
            {
                //changeState(PlayerState.Building);
                setChoosenItem();
                workingShop[currentSlot].GetComponent<outlineScript>().disableOutline();
                //TurretPlayerShop.SetActive(false);
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
               (Input.GetButtonDown("Player1_BackRight") ||
               Input.GetAxis("Player1_BackRight") == 1))
            {
                workingShop[currentSlot].GetComponent<outlineScript>().disableOutline();
                changeState(PlayerState.Ingame);
                SpellPlayerShop.SetActive(false);
            }
            else if (SpellPlayerShop.activeSelf == false &&
                    TurretPlayerShop.activeSelf == false &&
                    UnitPlayerShop.activeSelf == false &&
                    (Input.GetButtonDown("Player1_BackRight") ||
                    Input.GetAxis("Player1_BackRight") == 1))
            {
                changeState(PlayerState.Shop);
                SpellPlayerShop.SetActive(true);
                currentSlot = 0;
                copyArray(SpellSlotTable);
                workingShop[currentSlot].GetComponent<outlineScript>().enableOutline();

            }


            if (Input.GetButtonDown("Player1_Accept") &&
                    SpellPlayerShop.activeSelf == true &&
                    workingShop[currentSlot].GetComponent<spellButtonScript>().interactable == true)
            {
                setChoosenItem();
                workingShop[currentSlot].GetComponent<outlineScript>().disableOutline();
                //changeState(PlayerState.CastingSpell);
                //SpellPlayerShop.SetActive(false);
            }

        }
    }

    private void copyArray(GameObject[] tempArray)
    {
        for (int i = 0; i < tempArray.Length; i++)
        {
            workingShop[i] = tempArray[i];
        }

        Debug.Log("TESTWEE!");
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
