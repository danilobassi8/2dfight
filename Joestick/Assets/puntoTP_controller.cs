using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puntoTP_controller : MonoBehaviour
{
    public float radio = 0.2f;

    public bool dentroMapa;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (dentroMapa)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
    }
    void OnDrawGizmos() // despues borrar
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(this.gameObject.transform.position, radio);
    }



    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "LimitesDelMapa")
        {
            dentroMapa = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.name == "LimitesDelMapa")
        {
            dentroMapa = false;
        }
    }
}
