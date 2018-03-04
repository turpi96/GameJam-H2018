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
    public bool hasToRotate = true;
    public bool isAttackingTurret = false;

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
                if(hasToRotate)
                {
                    transform.Rotate(Vector3.forward * Time.deltaTime * 1200);
                }
                else
                {
                    Vector3 diff = target.transform.position - transform.position;
                    diff.Normalize();
                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                }
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
        if (hasToRotate)
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
        else
        {
            if (other.GetComponent<HasTeam>() != null)
            {
                if (team != other.GetComponent<HasTeam>().getTeam())
                {
                    if (isAttackingTurret )
                    {
                        if (other.GetComponent<CanBeHurt>() != null)
                        {
                            other.GetComponent<CanBeHurt>().Hurt(hurtValue);
                            Destroy(gameObject);
                        }
                    }
                    else if(other.tag =="Unit" || other.tag =="Tower")
                    {
                        if (other.GetComponent<CanBeHurt>() != null)
                        {
                            other.GetComponent<CanBeHurt>().Hurt(hurtValue);
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
    }

    public void Initialise(GameObject objectToFollow)
    {
        target = objectToFollow;
        isInit = true;
    }
}
