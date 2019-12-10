using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chidori_sequirPlayer : MonoBehaviour
{
    public bool test; //variable que use para testearlo.

    private string playerName;
    Vector3 posicionSeguir;


    void Start()
    {
        if (!test)
            playerName = this.name.Substring(0, 7); //veo quien es el player con el nombre del chidori.

    }

    void Update()
    {
        if (!test)
        {
<<<<<<< HEAD
            posicionSeguir = GameObject.Find(playerName).GetComponent<Transform>().Find("cuerpo").GetComponent<Transform>().Find("mano IZQ").transform.position;
=======
            posicionSeguir = GameObject.Find(playerName).GetComponent<Transform>().Find("mano IZQ").transform.position;
>>>>>>> 446d00c0996dfb4cabf43b54302eb25c03d9a940
            posicionSeguir.z = posicionSeguir.z -2;
            this.transform.position = posicionSeguir;

        }
    }
}
