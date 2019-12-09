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
    


    // Use this for initialization
    void Start()
    {
        clockInterno = TiempoEntreKunai;
    }

    // Update is called once per frame
    void Update()
    {
        if (puedeTirar)
        {
            if (clockInterno >= TiempoEntreKunai )
            {
                clockInterno = 0;
                InvocaKunai(this.transform.position);
                
            }
            clockInterno = clockInterno + Time.deltaTime;
        }


    }

    public void InvocaKunai(Vector3 posicion)
    {
        GameObject a = Instantiate(KunaiPrefab) as GameObject;
        a.transform.position = posicion;
        a.GetComponent<Kunai_Controller>().Objecto_Fundador = this.transform.parent.gameObject;
        a.GetComponent<Kunai_Controller>().direccionATirar = this.direccionATirar;
        Destroy(a, 3.0f);
    }
}
