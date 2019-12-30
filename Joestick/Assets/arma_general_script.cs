using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arma_general_script : MonoBehaviour
{

    public GameObject Invocador;
    public bool agarrada = false;
    public bool puedeDañar = false;
    public bool lanzada = false;

    // Use this for initialization
    void Start()
    {
        this.gameObject.tag = "Armas";

    }

    // Update is called once per frame
    void Update()
    {
        if (lanzada)
        {
            Invocador = null;
            agarrada = false;
        }
        else
        {

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (agarrada == false)
        {
            if (col.tag == "Player")
            {
                if (col.gameObject.transform.root.transform.Find("ManejadorArmas").GetComponent<ManejadorArmas_controller>().armado == false) // si no tiene una ya agarrada de antes.
                {
                    Invocador = col.transform.root.gameObject;
                    agarrada = true;
                    lanzada = false;

                    this.gameObject.transform.SetParent(null);
                    Invocador.transform.root.gameObject.transform.Find("ManejadorArmas").gameObject.GetComponent<ManejadorArmas_controller>().Armar(this.gameObject);

                    return;
                }

            }
        }
        else // si la tiene el player (invocador) en la mano.
        {

        }
    }

}
