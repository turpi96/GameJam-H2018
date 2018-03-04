using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unitButtonScript : MonoBehaviour {

    public GameObject myUnit;
    public Image buttonSprite;
    public Text statText;
    public Sprite unitSprite;
    public Text myMoneyText;

    public bool interactable;
    
    Unit unitScript;
    int buttonCost;

    // Use this for initialization
    void Start () {
        unitScript = myUnit.GetComponent<Unit>();

        buttonCost = unitScript.cost;

        buttonSprite.sprite = unitSprite;
        statText.text = unitScript.health.ToString() + "\n" +
                        unitScript.attack.ToString() + "\n" +
                        unitScript.defense.ToString() + "\n" +
                        unitScript.attackDelay.ToString() + "\n" +
                        unitScript.cost.ToString();

        checkMoneyButton();
    }
	
	// Update is called once per frame
	void Update () {
        checkMoneyButton();

    }

    public void checkMoneyButton()
    {
        int myMoney = int.Parse(myMoneyText.text);

        Debug.Log(buttonCost.ToString());
        //Debug.Log(myMoney.ToString());

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
        

        //Debug.Log(interactable);
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
