using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private GameObject target;
    private bool isInit = false;

    public string team;
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
                transform.Rotate(Vector3.forward * Time.deltaTime * 1200);
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
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
            if (team != other.GetComponent<HasTeam>().getTeam())
            {
                other.GetComponent<Unit>().Hurt(hurtValue);
                Destroy(gameObject);
            }
        }
    }

    public void Initialise(GameObject objectToFollow)
    {
        target = objectToFollow;
        isInit = true;
    }
}
