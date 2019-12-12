using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class chidori_particulas_controller : MonoBehaviour
{

    public GameObject master;


    void Start()
    {
        this.transform.position = master.transform.position;
        GameObject.Destroy(this,10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (master != null)
            this.transform.position = master.transform.position;


    }
}
