using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unitButtonScript : MonoBehaviour {

    public GameObject myUnit;
    public Text statText;

	// Use this for initialization
	void Start () {
        Unit unitScript = myUnit.GetComponent<Unit>();

        statText.text = unitScript.health.ToString() + "\n" +
                        unitScript.attack.ToString() + "\n" +
                        unitScript.defense.ToString() + "\n" +
                        unitScript.attackDelay.ToString() + "\n" +
                        unitScript.cost.ToString();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void checkMoneyButton(int myMoney)
    {
        /*if (myUnit.cost < myMoney)
            this.interactable = false;
        else
            this.interactable = true;*/
    } 
}
