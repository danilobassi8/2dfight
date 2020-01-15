using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Vida_Script : MonoBehaviour
{
    public float vida, tps, vidaAnterior, tpsAnterior;

    private GameObject imagenVida, imagenTPS;

    void Start()
    {
        vida = 100;
        tps = 100;
        vidaAnterior = vida;
        tpsAnterior = tps;

        //detecta su barra de vida y TPS.
        string nroSelecter = this.gameObject.name[this.gameObject.name.Length - 1].ToString();
        imagenVida = GameObject.Find("Canvas/Marcadores/Marcador" + nroSelecter + "/Vida");
        imagenTPS = GameObject.Find("Canvas/Marcadores/Marcador" + nroSelecter + "/TPS");

    }

    void Update()
    {
        if (vida != vidaAnterior || tps != tpsAnterior)
        {
            ActualizaMarcador();
        }
    }

    public void PierdeVida(float vidaPerdida)
    {
        vida = vida - vidaPerdida;
        ActualizaMarcador();
    }
    public void UtilizaTP()
    {
        tps = tps - 25;

    }

    public void ActualizaMarcador()
    {
        imagenVida.GetComponent<Image>().fillAmount = vida / 100;
        imagenTPS.GetComponent<Image>().fillAmount = tps / 100;
        vidaAnterior = vida;
        tpsAnterior = tps;
    }


}
