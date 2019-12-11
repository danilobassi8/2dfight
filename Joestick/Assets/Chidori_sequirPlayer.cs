using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chidori_sequirPlayer : MonoBehaviour
{
    public bool test; //variable que use para testearlo.

    private string playerName;
    Vector3 posicionSeguir;
    public GameObject PrefabParticulas;
    
    private GameObject particulas;


    void Start()
    {
        if (!test)
            playerName = this.name.Substring(0, 7); //veo quien es el player con el nombre del chidori.

        particulas = Instantiate(PrefabParticulas,this.gameObject.transform.position, Quaternion.identity) as GameObject;
        particulas.GetComponent<chidori_particulas_controller>().master = this.gameObject;
    }

    void Update()
    {
        if (!test)
        {
            posicionSeguir = GameObject.Find(playerName).GetComponent<Transform>().Find("cuerpo").GetComponent<Transform>().Find("mano IZQ").transform.position;
            posicionSeguir.z = posicionSeguir.z -2;
            this.transform.position = posicionSeguir;

        }
    }
}
