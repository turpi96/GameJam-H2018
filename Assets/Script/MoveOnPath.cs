using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPath : MonoBehaviour {

    public Path PathToFollow;
    public float speed = 1;
    public bool isReverse = false;

    private float currentSpeed;
    private int CurrentWayPointID = 0;
    private bool isFinished = false;
    private float reachDistance = 0f;
    private bool EndingValidation = false;

    //Vector3 last_position;
    //Vector3 current_position;

	void Start () {
        currentSpeed = speed;
        if(isReverse)
        {
            CurrentWayPointID = PathToFollow.path_objs.Count -1;
        }
	}
	
	// Update is called once per frame
	void Update () {
        float distance = 0 ;

        if (!isReverse)
        {
            if (CurrentWayPointID >= PathToFollow.path_objs.Count)
                EndingValidation = true;
        }
        else
        {
            if (CurrentWayPointID < 0)
                EndingValidation = true;
        }


        if (EndingValidation)
        {
            isFinished = true;
        }
        else
        {
            distance = Vector3.Distance(PathToFollow.path_objs[CurrentWayPointID].position, transform.position);
            if(!this.gameObject.GetComponent<Unit>().GetIsAttacking())
                transform.position = Vector3.MoveTowards(transform.position, PathToFollow.path_objs[CurrentWayPointID].position, Time.deltaTime * currentSpeed);
        }

        
        if (distance <= reachDistance && !isFinished)
        {
            if (isReverse)
            {
                CurrentWayPointID--;
            }
            else
            {
                CurrentWayPointID++;
            }
        }


	}

    public void MultiplySpeed(float speedPercentage)
    {
        currentSpeed = (currentSpeed * speedPercentage);
    }

    public void ResetSpeed()
    {
        currentSpeed = speed;
    }
}
