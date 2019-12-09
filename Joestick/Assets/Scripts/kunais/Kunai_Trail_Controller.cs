using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai_Trail_Controller : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // la posicion del la cola es la misma que la del kunai.
        this.transform.position = transform.parent.gameObject.transform.position;
    }
}
