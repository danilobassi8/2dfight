using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_seleccionPersonajes : MonoBehaviour
{

    private GameObject Players;
    private GameObject playerCuerpo;
    private Color negro;

    void Start()
    {
        negro = Color.black;
        negro.a = 1f;

        //me traigo todos los players y los pongo en negro.
        Players = GameObject.Find("Players");
        foreach (Transform Player in Players.transform)
        {
            PintarJugadorNegro(Player.gameObject);
        }
    }

    void Update()
    {

    }


    void PintarJugadorNegro(GameObject player)
    {
        // Este metodo pinta de negro un jugador recibido por parametro.

        playerCuerpo = player.transform.Find("cuerpo").gameObject;
        playerCuerpo.transform.Find("CARA").GetComponent<SpriteRenderer>().color = negro;
        playerCuerpo.transform.Find("Cuerpo").GetComponent<SpriteRenderer>().color = negro;
        playerCuerpo.transform.Find("mano DER").GetComponent<SpriteRenderer>().color = negro;
        playerCuerpo.transform.Find("mano IZQ").GetComponent<SpriteRenderer>().color = negro;


    }
}
