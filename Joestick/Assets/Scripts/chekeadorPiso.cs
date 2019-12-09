using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chekeadorPiso : MonoBehaviour
{

    private GameObject padre;
    public bool tocandoPiso;
    public string ladoColision;

    // Use this for initialization
    void Start()
    {
        padre = transform.root.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Mapa")
        {
            tocandoPiso = true;
            if (col.transform.position.x > this.transform.position.x)
            {
                ladoColision = "DERECHA";
            }
            else
            {
                ladoColision = "IZQUIERDA";
            }

        }

    }
    void OnTriggerExit2D(Collider2D col)
    {
        tocandoPiso = false;
    }
}
