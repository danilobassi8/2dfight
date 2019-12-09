using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai_Spawner_Controller : MonoBehaviour
{

    public float cantidadMaxKunai;
    public float TiempoEntreKunai;
    public GameObject KunaiPrefab;
    public bool Puede_Tirar;

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
        
        if (Puede_Tirar && clockInterno >= TiempoEntreKunai && contadorInterno<= cantidadMaxKunai)
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
        a.GetComponent<Kunai_Controller>().Objecto_Fundador = this.transform.parent.gameObject;
        a.GetComponent<Kunai_Controller>().direccionIR = this.transform.parent.gameObject.transform.GetComponent<Player_Ataques>().joestick.direccionJoestickDerecho;
        Destroy(a, 3.0f);
    }
}
