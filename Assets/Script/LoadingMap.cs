using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingMap : MonoBehaviour {

	public float timeToWait = 3.87f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timeToWait -= Time.deltaTime;
		if (timeToWait <= 0.0f) {
			SceneManager.LoadScene ("Menu");
		}
	}
}
