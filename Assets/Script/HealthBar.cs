using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float scalex = (float)GetComponentInParent<Unit>().health / (float)GetComponentInParent<Unit>().GetMaxHealth();
        transform.localScale = new Vector3(scalex, 1, 1);
    }
}
