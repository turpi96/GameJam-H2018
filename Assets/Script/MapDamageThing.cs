using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDamageThing : MonoBehaviour
{

    public Sprite explosion;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(Explode());
        Destroy(gameObject, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(1.0f);
        GetComponent<SpriteRenderer>().sprite = explosion;
    }
}
