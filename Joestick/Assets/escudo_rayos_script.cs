using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escudo_rayos_script : MonoBehaviour
{
    public float velocidadGiro;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 0, velocidadGiro*Time.deltaTime);
    }
}
