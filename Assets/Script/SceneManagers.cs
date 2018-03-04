using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagers : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void quitGame(){
		Application.Quit ();
	}

	public void startGame(){
		SceneManager.LoadScene ("ButtonLayout");


	}

	public void ReturnToMenu(){
		SceneManager.LoadScene ("Menu");
	}
}
