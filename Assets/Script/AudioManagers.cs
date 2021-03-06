﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagers : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		foreach (AudioManagers c in FindObjectsOfType<AudioManagers> ()) {
			if (c != this)
				Destroy (c.gameObject);
		}
	}
		
}
