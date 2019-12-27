using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai_Spawner_Controller : MonoBehaviour
{


    public float TiempoEntreKunai;
    public GameObject KunaiPrefab;
    public bool puedeTirar;
    public Vector3 direccionATirar;
    public float CantidadKunaisSinPadre;

    private float clockInterno;
    private _Joestick Joestick_del_Padre;
    private float contadorKunais;
    private Color color;



    // Use this for initialization
    void Start()
    {
        clockInterno = TiempoEntreKunai;
        if (transform.parent != null)
        {
            Joestick_del_Padre = this.transform.parent.GetComponent<Player1_controller>().joestick;
            contadorKunais = 30;
            color = this.transform.parent.GetComponent<Player_Ataques>().colorChidori;

        }
        else //si el padre es null.
        {
            contadorKunais = CantidadKunaisSinPadre;
            color = Color.white;
        }

        color.a = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null)
            this.direccionATirar = Joestick_del_Padre.direccionJoestickDerecho;
        else                                                                        //si no tiene padre, es porque sale derecho.
            this.direccionATirar = new Vector3(1, 0, 0);

        if (puedeTirar)
        {
            if (clockInterno >= TiempoEntreKunai)
            {
                clockInterno = 0;
                InvocaKunai(this.transform.position, direccionATirar);

                if (transform.parent == null)
                    contadorKunais = contadorKunais - 1;
            }
            clockInterno = clockInterno + Time.deltaTime;
        }

        if (contadorKunais <= 0)
            puedeTirar = false;
    }

    public void InvocaKunai(Vector3 posicion, Vector3 dondeTiro)
    {
        GameObject a = Instantiate(KunaiPrefab) as GameObject;
        a.transform.position = posicion;
        a.GetComponent<SpriteRenderer>().color = color;
        if (transform.parent != null)
            a.GetComponent<Kunai_Controller>().Objecto_Fundador = this.transform.parent.gameObject;
        a.GetComponent<Kunai_Controller>().direccionATirar = dondeTiro;

    }
}
