using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turretButtonScript : MonoBehaviour {

    public GameObject myTurret;
    public Image buttonSprite;
    public Text statText;
    public Sprite towerSprite;
    public Text myMoneyText;

    public bool interactable;

    Building turretScript;
    int buttonCost;

    public bool customText = false;

    // Use this for initialization
    void Start()
    {

        turretScript = myTurret.GetComponent<Building>();

        buttonCost = turretScript.cost;

        buttonSprite.sprite = towerSprite;

        if (!customText)
        {
            statText.text = turretScript.health.ToString() + "\n" +
                        myTurret.GetComponent<Turret>().bulletToShoot.GetComponent<Bullet>().hurtValue.ToString() + "\n" +
                        turretScript.defense.ToString() + "\n" +
                        turretScript.GetComponent<Turret>().shootingDelay.ToString() + "\n" +
                        turretScript.cost.ToString();
        }
        else
        {

            statText.text = turretScript.health.ToString() + "\n" +
                        turretScript.defense.ToString() + "\n" +
                        turretScript.cost.ToString();
        }

        checkMoneyButton();
    }

    // Update is called once per frame
    void Update()
    {
        checkMoneyButton();

    }

    public void checkMoneyButton()
    {
		Player p;
		if (myTurret.GetComponent<Building> ().getTeam () == "p1") {
			p = GameObject.FindObjectOfType<FirstPlayer> ();
		}else
			p = GameObject.FindObjectOfType<SecondPlayer> ();
		int myMoney = p.money;

        if (turretScript.cost > myMoney)
            interactable = false;
        else
            interactable = true;


        grayOutButton();
    }

    void grayOutButton()
    {
        Image imgButton = base.GetComponent<Image>();
        Color btnColor = imgButton.color;


        //Debug.Log(interactable);
        if (interactable == false)
            btnColor.a = 0.5f;
        else
            btnColor.a = 1f;

        imgButton.color = btnColor;


    }

    public GameObject returnSelectedObject()
    {
        return myTurret;
    }
}
