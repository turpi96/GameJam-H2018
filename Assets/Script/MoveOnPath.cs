using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPath : MonoBehaviour {

    public Path PathToFollow;

    public int CurrentWayPointID = 0;
    public float speed = 1;
    private float reachDistance = 1.0f;
    public string pathName;
    private bool isFinished = false;

    //Vector3 last_position;
    Vector3 current_position;

	void Start () {
        //PathToFollow = GameObject.Find(pathName).GetComponent<Path>();
        //last_position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        float distance = 0 ;

        if (CurrentWayPointID >= PathToFollow.path_objs.Count)
        {
            isFinished = true;
        }
        else
        {
            distance = Vector3.Distance(PathToFollow.path_objs[CurrentWayPointID].position, transform.position);
            transform.position = Vector3.MoveTowards(transform.position, PathToFollow.path_objs[CurrentWayPointID].position, Time.deltaTime * speed);
        }

        if (distance <= reachDistance && !isFinished)
        {
            CurrentWayPointID++;
        }


	}
}
