using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Marcadores_Script : MonoBehaviour
{
    private GameObject Player;
    private Color colorJugador;
    private GameObject tazos;

    void Start()
    {
        // determina que Players estan referenciados..
        string nroSelecter = this.gameObject.name[this.gameObject.name.Length - 1].ToString();
        Player = GameObject.Find("Player" + nroSelecter);

        // si no esta el player indicado. Se oculta.
        if (Player == null)
            this.gameObject.SetActive(false);
        else // todo lo que hace si existe el player.
        {
            colorJugador = Player.GetComponent<Player_Ataques>().colorChidori;
            colorJugador.a = 1f;
            this.transform.Find("Colores").GetComponent<Image>().color = colorJugador;

            TazoActivo(Player.GetComponent<Player_Skin_Controller>().Skins[Player.GetComponent<Player_Skin_Controller>().SelectedSkin].Nombre);
            //mando por parametro el nombre del skin seleccionado del player.
        }
    }

    void Update()
    {

    }

    public void TazoActivo(string nombreTazo)
    {
        //desactiva todos los tazos y activa solamente el que tiene el mismo nombre.
        tazos = this.transform.Find("Tazos").gameObject;

        foreach (Transform tazo in tazos.transform)
        {
            if (tazo.gameObject.name == nombreTazo)
            {
                tazo.gameObject.SetActive(true);
            }
            else
                tazo.gameObject.SetActive(false);
        }

        if (nombreTazo == "Amy")
            this.transform.Find("Amy_suplement").gameObject.SetActive(true);
        else
            this.transform.Find("Amy_suplement").gameObject.SetActive(false);


    }
}
