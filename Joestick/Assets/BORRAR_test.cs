using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BORRAR_test : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("ENTRO:" + col.gameObject.transform.root.name);
    }
    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("SALIO" + col.gameObject.transform.root.name);

    }
    void OnTriggerStay2D(Collider2D col)
    {
		
    }
}
