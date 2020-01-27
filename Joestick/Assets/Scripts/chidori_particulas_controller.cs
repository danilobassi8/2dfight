using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class chidori_particulas_controller : MonoBehaviour
{

    public GameObject master;
    public bool encandido;
    public Color color;

    private Vector3 posicion;
    private ParticleSystem ps;

    void Start()
    {
        this.gameObject.name = "Player" + master.name[6] + "_ParticulasChidori";

        this.transform.position = master.transform.position;

        //cambia el startColor"
        ps = GetComponent<ParticleSystem>();
        var main = ps.main;
        color.a = 1f;
        main.startColor = color;


        if (this.gameObject.name != "seguidorMano")
            GameObject.Destroy(this.gameObject, 8f);


    }

    // Update is called once per frame
    void Update()
    {
        if (master != null)
        {
            if (this.gameObject.name == "seguidorMano")
            {
                posicion = master.transform.position;
                posicion.z = posicion.z + 5;
                this.transform.position = posicion;
            }
            else // si es el efecto del chidori normal.
            {
                posicion = master.transform.position;
                posicion.z = posicion.z + 5;
                this.transform.position = posicion;
            }

        }




    }
}
