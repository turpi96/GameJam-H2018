using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private GameObject target;
    private bool isInit = false;

    public float speed = 10.0f;
    public float maxLiveTime = 10.0f;
    public int hurtValue = 3;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, maxLiveTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInit)
        {
            if(target != null)
            {
                //transform.right = target.transform.position - transform.position;
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
                /*if (transform.position == target.transform.position)
                {
                    Destroy(gameObject);
                }*/
            }
            else
            {
                Destroy(gameObject);
            }

        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Unit>() != null)
        {
            other.GetComponent<Unit>().Hurt(hurtValue);
            Destroy(gameObject);
        }
    }

    public void Initialise(GameObject objectToFollow)
    {
        target = objectToFollow;
        isInit = true;
    }
}
