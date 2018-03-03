using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlayer : Player {

	// Use this for initialization
	new public void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	new public void Update () {
		base.Update ();

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
                

                break;

			}
	}

	public override void checkPosition(){
		Vector2 halfSize;
		switch (playerState) {
		case PlayerState.Building:
			halfSize.x = currentlyBuilding.GetComponent<SpriteRenderer> ().bounds.size.x * 25;
			halfSize.y = currentlyBuilding.GetComponent<SpriteRenderer> ().bounds.size.y * 25;
			break;
		default:
			halfSize.x = GetComponent<SpriteRenderer> ().bounds.size.x * 25;
			halfSize.y = GetComponent<SpriteRenderer> ().bounds.size.y * 25;
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
			Instantiate (bomb, posCam, Quaternion.identity,transform);
		}
	}

    private void checkShopInput()
    {
      if (UnitPlayerShop.active == true && Input.GetButtonDown("Player2_Left"))
        {
            changeState(PlayerState.Ingame);
            UnitPlayerShop.SetActive(false);
        }
        else if (UnitPlayerShop.active == false && Input.GetButtonDown("Player2_Left"))
        {
            changeState(PlayerState.Shop);
            UnitPlayerShop.SetActive(true);
        }
    }
}
