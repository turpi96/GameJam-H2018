using System.Collections;
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
                    workingShop[currentSlot].GetComponent<outlineScript>().disableOutline();


                    if (Input.GetAxis("Player2_Up") > 0)
                        currentSlot--;
                    else if (Input.GetAxis("Player2_Up") < 0)
                        currentSlot++;
                    if (currentSlot < 0)
                        currentSlot = 2;
                    if (currentSlot > 2)
                        currentSlot = 0;

                    workingShop[currentSlot].GetComponent<outlineScript>().enableOutline();
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

	/*public override void spawnBomb(){
		if (Input.GetMouseButtonDown (0)) {
			Vector3 mousePos = Input.mousePosition;
			Vector3 posCam = cam.ScreenToWorldPoint (mousePos);
			posCam.z = 0;
			Casting g = Instantiate (bomb, posCam, Quaternion.identity);
			g.tag = transform.tag;
		}
	}*/

    private void checkShopInput()
    {
<<<<<<< HEAD
		if (UnitPlayerShop != null) {
			if (UnitPlayerShop.activeSelf == true &&
			   TowerPlayerShop.activeSelf == false &&
			   Input.GetButtonDown ("Player2_Left")) {
                Debug.Log("I'VE BEEN THERE");
                workingShop [currentSlot].GetComponent<outlineScript> ().disableOutline ();

				changeState (PlayerState.Ingame);
				UnitPlayerShop.SetActive (false);
			} else if (UnitPlayerShop.activeSelf == false &&
			          TowerPlayerShop.activeSelf == false &&
			          Input.GetButtonDown ("Player2_Left"))
            {
                changeState(PlayerState.Shop);
                UnitPlayerShop.SetActive(true);
                currentSlot = 0;
				copyArray (UnitSlotTable);
				workingShop [currentSlot].GetComponent<outlineScript> ().enableOutline ();


			}


			if (Input.GetButtonDown ("Player2_Accept") &&
			        UnitPlayerShop.activeSelf == true &&
			        workingShop [currentSlot].GetComponent<unitButtonScript> ().interactable == true)
            {
				setChoosenItem ();
			}
=======
		if(UnitPlayerShop != null){
		if (UnitPlayerShop.activeSelf == true &&
		    TowerPlayerShop.activeSelf == false &&
		    Input.GetButtonDown ("Player2_Left")) {
			workingShop [currentSlot].GetComponent<unitButtonScript> ().disableOutline ();

			changeState (PlayerState.Ingame);
			UnitPlayerShop.SetActive (false);
		} else if (UnitPlayerShop.activeSelf == false &&
		               TowerPlayerShop.activeSelf == false &&
		               Input.GetButtonDown ("Player2_Left")) {
			currentSlot = 0;
			copyArray (UnitSlotTable);
			workingShop [currentSlot].GetComponent<unitButtonScript> ().enableOutline ();

>>>>>>> 29479fe3622b9eda4061f8c4d8e3291f1225e541
		}
       if (Input.GetButtonDown("Player2_Accept") && 
            UnitPlayerShop.activeSelf == true &&
            workingShop[currentSlot].GetComponent<unitButtonScript>().interactable == true)
        {
            setChoosenItem();
        }

    	}	
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
