﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpell : MonoBehaviour {

    public int attack = 40;
    public float speed = 10.0f;
    public string team;

    private Vector3 target;
    GameObject player;
    bool move = false;
    // Use this for initialization
    void Start () {
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        
        if (team == "p1")
        {
            player = GameObject.FindObjectOfType<FirstPlayer>().gameObject;
        }
        else
        {
            player = GameObject.FindObjectOfType<SecondPlayer>().gameObject;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!move)
        {
            if (Input.GetButtonDown("Player2_Accept") || Input.GetKeyDown(KeyCode.F))
            {
                target = player.transform.position;
                Vector3 between = target - transform.position;
                between.z = 0;
                target += between * 1000;
                move = true;
                GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                StartCoroutine(DestroyTimer());
            }

            Vector3 diff = player.transform.position - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        }
        else
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HasTeam>() != null)
        {
            if (other.GetComponent<CanBeHurt>() != null && team != other.GetComponent<HasTeam>().getTeam())
            {
                other.GetComponent<CanBeHurt>().Hurt(attack);
            }
        }
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(gameObject);
    }
}
