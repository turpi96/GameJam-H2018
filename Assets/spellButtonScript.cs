using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spellButtonScript : MonoBehaviour {

    public GameObject mySpell;
    public Image buttonSprite;
    public Text statText;
    public Sprite spellSprite;
    public Text myMoneyText;

    public bool interactable;

    //Building turretScript;
    //int buttonCost;

    //public bool customText = false;

    // Use this for initialization
    void Start()
    {

        //turretScript = myTurret.GetComponent<Building>();

        //buttonCost = turretScript.cost;

        buttonSprite.sprite = spellSprite;

        if (mySpell.GetComponent<Bomb>() != null)
            statText.text = mySpell.GetComponent<Bomb>().cost.ToString();
   
        checkMoneyButton();
    }

    // Update is called once per frame
    void Update()
    {
        checkMoneyButton();

    }

    public void checkMoneyButton()
    {
        int myMoney = int.Parse(myMoneyText.text);

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
