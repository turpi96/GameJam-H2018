using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class outlineScript : MonoBehaviour {

    public void enableOutline()
    {
        Outline myOutline = GetComponent<Outline>();
        myOutline.enabled = true;
    }

    public void disableOutline()
    {
        Outline myOutline = GetComponent<Outline>();
        myOutline.enabled = false;
    }
}
