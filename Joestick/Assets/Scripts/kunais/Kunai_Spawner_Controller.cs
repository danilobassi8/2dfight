using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai_Spawner_Controller : MonoBehaviour
{


    public float TiempoEntreKunai;
    public GameObject KunaiPrefab;
    public bool puedeTirar;
    public Vector3 direccionATirar;

    private float clockInterno;
    private _Joestick Joestick_del_Padre;



    // Use this for initialization
    void Start()
    {
        clockInterno = TiempoEntreKunai;
        Joestick_del_Padre = this.transform.parent.GetComponent<Player1_controller>().joestick;
    }

    // Update is called once per frame
    void Update()
    {
        this.direccionATirar = Joestick_del_Padre.direccionJoestickDerecho;

        if (puedeTirar)
        {
            if (clockInterno >= TiempoEntreKunai)
            {
                clockInterno = 0;
                InvocaKunai(this.transform.position, direccionATirar);

            }
            clockInterno = clockInterno + Time.deltaTime;
        }


    }

    public void InvocaKunai(Vector3 posicion, Vector3 dondeTiro)
    {
        GameObject a = Instantiate(KunaiPrefab) as GameObject;
        a.transform.position = posicion;
        a.GetComponent<Kunai_Controller>().Objecto_Fundador = this.transform.parent.gameObject;
        a.GetComponent<Kunai_Controller>().direccionATirar = dondeTiro;

    }
}
