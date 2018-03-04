using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spellButtonScript : MonoBehaviour {

    public GameObject mySpell;
    public Image buttonSprite;
    public Text[] statText;
    public Sprite spellSprite;
    public Text myMoneyText;

    public bool interactable;


    // Use this for initialization
    void Start()
    {

        //buttonSprite.sprite = spellSprite;

        if (mySpell.GetComponent<Bomb>() != null)
        {
            statText[0].text = mySpell.GetComponent<Bomb>().attack.ToString();
            statText[1].text = mySpell.GetComponent<Bomb>().cost.ToString();
        }

        if (mySpell.GetComponent<ArrowSpell>() != null)
        {
            statText[0].text = mySpell.GetComponent<ArrowSpell>().attack.ToString();
            statText[1].text = mySpell.GetComponent<ArrowSpell>().cost.ToString();
        }

        if (mySpell.GetComponent<AllMapDamage>() != null)
        {
            statText[0].text = mySpell.GetComponent<AllMapDamage>().damage.ToString();
            statText[1].text = mySpell.GetComponent<AllMapDamage>().cost.ToString();
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
		Player p = null;
		if (mySpell.GetComponent<Bomb> () != null) {
			if (mySpell.GetComponent<Bomb> ().getTeam () == "p1") {
				p = GameObject.FindObjectOfType<FirstPlayer> ();
			}else
				p = GameObject.FindObjectOfType<SecondPlayer> ();
		}
		if (mySpell.GetComponent<ArrowSpell> () != null) {
			if (mySpell.GetComponent<ArrowSpell> ().team  == "p1") {
				p = GameObject.FindObjectOfType<FirstPlayer> ();
			}else
				p = GameObject.FindObjectOfType<SecondPlayer> ();
		}
		if (mySpell.GetComponent<AllMapDamage> () != null) {
			if (mySpell.GetComponent<AllMapDamage> ().team  == "p1") {
				p = GameObject.FindObjectOfType<FirstPlayer> ();
			}else
				p = GameObject.FindObjectOfType<SecondPlayer> ();
		}


		int myMoney = 0;

		if(p != null)

		myMoney = p.money;




        if (mySpell.GetComponent<Bomb>() != null && mySpell.GetComponent<Bomb>().cost > myMoney)
            interactable = false;

        else if (mySpell.GetComponent<ArrowSpell>() != null && mySpell.GetComponent<ArrowSpell>().cost > myMoney)
            interactable = false;

        else if (mySpell.GetComponent<AllMapDamage>() != null && mySpell.GetComponent<AllMapDamage>().cost > myMoney)
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
        return mySpell;
    }
}
