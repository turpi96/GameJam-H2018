using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonManager : MonoBehaviour {
	public GameObject p1ButtonA;
	public GameObject p1ButtonB;
	public GameObject p1ButtonX;
	public GameObject p1ButtonY;

	public GameObject p2ButtonA;
	public GameObject p2ButtonB;
	public GameObject p2ButtonX;
	public GameObject p2ButtonY;

	FirstPlayer firstPlayer;
	SecondPlayer secondPlayer;
	// Use this for initialization
	void Start () {
		firstPlayer = FindObjectOfType<FirstPlayer> ();
		secondPlayer = FindObjectOfType < SecondPlayer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (firstPlayer.SpellPlayerShop.activeSelf || firstPlayer.TurretPlayerShop.activeSelf || firstPlayer.UnitPlayerShop.activeSelf) {
			p1ButtonX.SetActive (false);
			p1ButtonY.SetActive (false);
			p1ButtonA.SetActive (true);
			p1ButtonB.SetActive (true);
			p1ButtonA.transform.localPosition = new Vector3 (-200, 125, 0);
			p1ButtonB.transform.localPosition = new Vector3 (-150, 125, 0);
			p1ButtonB.GetComponentInChildren<Text> ().text = "Cancel";
			p1ButtonA.GetComponentInChildren<Text> ().text = "Buy";
		} else if (firstPlayer.playerState == Player.PlayerState.Building || firstPlayer.playerState == Player.PlayerState.Arrow || firstPlayer.playerState == Player.PlayerState.CastingSpell)  {
			p1ButtonX.SetActive (false);
			p1ButtonY.SetActive (false);
			p1ButtonA.SetActive (true);
			p1ButtonB.SetActive (true);
			p1ButtonA.transform.localPosition = new Vector3 (-200, 145, 0);
			p1ButtonB.transform.localPosition = new Vector3 (-200, 125, 0);
			p1ButtonB.GetComponentInChildren<Text> ().text = "Cancel";
			p1ButtonA.GetComponentInChildren<Text> ().text = "Confirm";

		}

		else {
			p1ButtonX.SetActive (true);
			p1ButtonY.SetActive (true);
			p1ButtonA.SetActive (false);
			p1ButtonB.transform.localPosition = new Vector3 (-270, 150, 0);
			p1ButtonB.GetComponentInChildren<Text> ().text = "Building Shop";

		}










		if (secondPlayer.SpellPlayerShop.activeSelf || secondPlayer.TurretPlayerShop.activeSelf || secondPlayer.UnitPlayerShop.activeSelf) {
			p2ButtonX.SetActive (false);
			p2ButtonY.SetActive (false);
			p2ButtonA.SetActive (true);
			p2ButtonB.SetActive (true);
			p2ButtonA.transform.localPosition = new Vector3 (115, -115, 0);
			p2ButtonB.transform.localPosition = new Vector3 (165, -115, 0);
			p2ButtonB.GetComponentInChildren<Text> ().text = "Cancel";
			p2ButtonA.GetComponentInChildren<Text> ().text = "Buy";
		} else if (secondPlayer.playerState == Player.PlayerState.Building || secondPlayer.playerState == Player.PlayerState.Arrow || secondPlayer.playerState == Player.PlayerState.CastingSpell) {
			p2ButtonX.SetActive (false);
			p2ButtonY.SetActive (false);
			p2ButtonA.SetActive (true);
			p2ButtonB.SetActive (true);
			p2ButtonA.transform.localPosition = new Vector3 (115, -115, 0);
			p2ButtonB.transform.localPosition = new Vector3 (115, -135, 0);
			p2ButtonB.GetComponentInChildren<Text> ().text = "Confirm";
			p2ButtonA.GetComponentInChildren<Text> ().text = "Buy";

		}


		else {
			p2ButtonX.SetActive (true);
			p2ButtonY.SetActive (true);
			p2ButtonA.SetActive (false);
			p2ButtonB.transform.localPosition = new Vector3 (215, -175, 0);
			p2ButtonB.GetComponentInChildren<Text> ().text = "Building Shop";


		}
	}
}
