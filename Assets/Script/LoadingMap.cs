using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingMap : MonoBehaviour {

	public float timeToWait = 3.87f;
	public bool loadingMap = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(loadingMap){
				timeToWait -= Time.deltaTime;
				if (timeToWait <= 0.0f) {
					SceneManager.LoadScene ("Menu");
				}
		}else if(Input.GetButton("Player1_A") || Input.GetButtonDown("Player2_A")){
			SceneManager.LoadScene("Gamejam");

		}
	}
}
