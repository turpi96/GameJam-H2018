using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unitButtonScript : MonoBehaviour {

    public GameObject myUnit;
    public Image buttonSprite;
    public Text[] statText;
    public Sprite unitSprite;
    public Text myMoneyText;

    public bool interactable;
    
    Unit unitScript;
    int buttonCost;

    // Use this for initialization
    void Start () {
        unitScript = myUnit.GetComponent<Unit>();

        buttonCost = unitScript.cost;

        statText[0].text = unitScript.health.ToString();
        statText[1].text = unitScript.attack.ToString();
        statText[2].text = unitScript.defense.ToString();
        statText[3].text = unitScript.attackDelay.ToString();
        statText[4].text = unitScript.GetComponent<MoveOnPath>().speed.ToString();
        statText[5].text = unitScript.health.ToString();


        checkMoneyButton();
    }
	
	// Update is called once per frame
	void Update () {
        checkMoneyButton();

    }

    public void checkMoneyButton()
    {
        int myMoney = int.Parse(myMoneyText.text);

        if (unitScript.cost > myMoney)
            interactable = false;
        else
            interactable = true;


        grayOutButton();
    }

    void grayOutButton()
    {
        Image imgButton = base.GetComponent<Image>();
        Color btnColor = imgButton.color;
        
        /////////////////////*******************LE CHANGEMENT D'ALPHA N'EST PAS VISIBLE**********************////////////////////////////
        if (interactable == false)
            btnColor.a = 0.5f;
        else
            btnColor.a = 1f;

        imgButton.color = btnColor;


    }

    public GameObject returnSelectedObject()
    {
        return myUnit;
    }

}
