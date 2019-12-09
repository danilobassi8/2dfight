using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piesTocando : MonoBehaviour
{
    public bool piesTocandoPiso;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay2D(Collider2D col)
    {

        piesTocandoPiso = true;


    }
    void OnTriggerExit2D(Collider2D col)
    {
        piesTocandoPiso = false;
    }
}
