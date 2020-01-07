using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_seleccionPersonajes : MonoBehaviour
{

    private GameObject Players;
    private GameObject playerCuerpo;

    private _Joestick joestick1, joestick2, joestick3, joestick4;
    private float timer;

    void Start()
    {
        //me traigo todos los players y los pongo en negro.
        Players = GameObject.Find("Players");
        foreach (Transform Player in Players.transform)
        {
            PintarJugador(Player.gameObject, Color.black);
        }

        //Referencio todos los joesticks.
        joestick1 = GameObject.Find("Joestick Controller").GetComponent<InputController>().Joestick1;
        joestick2 = GameObject.Find("Joestick Controller").GetComponent<InputController>().Joestick2;
        joestick3 = GameObject.Find("Joestick Controller").GetComponent<InputController>().Joestick3;
        joestick4 = GameObject.Find("Joestick Controller").GetComponent<InputController>().Joestick4;
    }

    void Update()
    {
        //chekea si los jugadores estan start o no.
        ChekeaHabilitacion(joestick1);
        ChekeaHabilitacion(joestick2);
        ChekeaHabilitacion(joestick3);
        ChekeaHabilitacion(joestick4);

        if (timer >= 0)
            timer -= Time.deltaTime;
    }

    public void ChekeaHabilitacion(_Joestick joestick)
    {
        if (joestick.Start)
        {
            PintarJugador(GameObject.Find("Players/Player" + joestick.numero.ToString()), Color.white);
            GameObject.Find("Selecter" + joestick.numero.ToString()).GetComponent<selecter_script>().habilitado = true;
        }
        if (joestick.b2)
        {
            if (GameObject.Find("Selecter" + joestick.numero.ToString()).GetComponent<selecter_script>().locked == true)
            {
                timer = 0.8f;
            }
            if (timer <= 0)
            {
                PintarJugador(GameObject.Find("Players/Player" + joestick.numero.ToString()), Color.black);
                PintaSelecter(GameObject.Find("Selecter" + joestick.numero.ToString()), Color.black);
                GameObject.Find("Selecter" + joestick.numero.ToString()).GetComponent<selecter_script>().habilitado = false;
            }

        }
    }

    public void PintarJugador(GameObject player, Color color)
    {
        // Este metodo pinta de un color al Player recibido por parametro.
        color.a = 1f;

        playerCuerpo = player.transform.Find("cuerpo").gameObject;
        playerCuerpo.transform.Find("CARA").GetComponent<SpriteRenderer>().color = color;
        playerCuerpo.transform.Find("Cuerpo").GetComponent<SpriteRenderer>().color = color;
        playerCuerpo.transform.Find("mano DER").GetComponent<SpriteRenderer>().color = color;
        playerCuerpo.transform.Find("mano IZQ").GetComponent<SpriteRenderer>().color = color;
    }

    public void PintaSelecter(GameObject selecter, Color color)
    {
        color.a = 1f;

        selecter.transform.Find("triangulo DER").GetComponent<SpriteRenderer>().color = color;
        selecter.transform.Find("triangulo IZQ").GetComponent<SpriteRenderer>().color = color;
        selecter.transform.Find("box").GetComponent<SpriteRenderer>().color = color;
    }

    private void ReproduceSonido(string nombreSonido)
    {
        this.gameObject.GetComponent<AudioManagerScript>().Play(nombreSonido);
    }
}
