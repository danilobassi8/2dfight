using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai_Spawner_Controller : MonoBehaviour
{

    public float cantidadMaxKunai;
    public float TiempoEntreKunai;
    public GameObject KunaiPrefab;

    private float clockInterno;
    private float contadorInterno;

    // Use this for initialization
    void Start()
    {
        clockInterno = TiempoEntreKunai;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (clockInterno >= TiempoEntreKunai && contadorInterno<= cantidadMaxKunai)
        {
            clockInterno = 0;
            InvocaKunai(this.transform.position);
            contadorInterno++;
        }
        clockInterno = clockInterno + Time.deltaTime;
        
    }

    public void InvocaKunai(Vector3 posicion)
    {
        GameObject a = Instantiate(KunaiPrefab) as GameObject;
        a.transform.position = posicion;
        Destroy(a, 3.0f);
    }
}
