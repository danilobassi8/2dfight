using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class chidori_particulas_controller : MonoBehaviour
{

    public GameObject master;

    private Vector3 posicion;

    void Start()
    {

        this.transform.position = master.transform.position;
        GameObject.Destroy(this.gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (master != null)
        {
            posicion = master.transform.position;
            posicion.z = posicion.z + 5;
            this.transform.position = posicion;
        }



    }
}
