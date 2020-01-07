using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selecter_script : MonoBehaviour
{
    public float tiempoEntreSeleccion;

    private _Joestick joestick;
    private GameObject player;
    private float timer;
    public Color colorJugador;
    public bool habilitado;
    public bool locked;


    void Start()
    {
        timer = tiempoEntreSeleccion;
        habilitado = false;



        // determina que Joestick y que Players estan referenciados..
        string nroSelecter = this.gameObject.name[this.gameObject.name.Length - 1].ToString();
        player = GameObject.Find("Players/Player" + nroSelecter);
        if (nroSelecter == "1")
        {
            joestick = GameObject.Find("Joestick Controller").GetComponent<InputController>().Joestick1;
        }
        else if (nroSelecter == "2")
        {
            joestick = GameObject.Find("Joestick Controller").GetComponent<InputController>().Joestick2;
        }
        else if (nroSelecter == "3")
        {
            joestick = GameObject.Find("Joestick Controller").GetComponent<InputController>().Joestick3;
        }
        else if (nroSelecter == "4")
        {
            joestick = GameObject.Find("Joestick Controller").GetComponent<InputController>().Joestick4;
        }

        PintaSelecter();
    }

    void Update()
    {
        if (timer <= 0 && habilitado)
        {
            if (joestick.fderecha || joestick.direccionJoestickDerecho.x > 0)
            {
                player.GetComponent<Player_Skin_Controller>().SelectedSkin = player.GetComponent<Player_Skin_Controller>().SelectedSkin + 1;
                timer = tiempoEntreSeleccion;
                Invoke("PintaSelecter", 0.05f);
            }
            if (joestick.fizquierda || joestick.direccionJoestickDerecho.x < 0)
            {
                player.GetComponent<Player_Skin_Controller>().SelectedSkin = player.GetComponent<Player_Skin_Controller>().SelectedSkin - 1;
                timer = tiempoEntreSeleccion;
                Invoke("PintaSelecter", 0.05f);
            }
        }
        else //mayor a 0
        {
            timer -= Time.deltaTime;
        }

        if (habilitado)
        {
            Invoke("PintaSelecter", 0.05f);
        }
        else
        {
            PintaSelecter(Color.black);
        }


    }

    public void PintaSelecter()
    {
        // Metodo que pinta el selecter del color del jugador.
        Color color = SeleccionDeColor(player.GetComponent<Player_Skin_Controller>().SelectedSkin);

        this.colorJugador = color;
        this.gameObject.transform.Find("triangulo DER").GetComponent<SpriteRenderer>().color = color;
        this.gameObject.transform.Find("triangulo IZQ").GetComponent<SpriteRenderer>().color = color;
        this.gameObject.transform.Find("box").GetComponent<SpriteRenderer>().color = color;
    }
    public void PintaSelecter(Color color)
    {
        // Metodo que pinta el selecter del color del jugador.
        color.a = 1f;
        this.colorJugador = color;
        this.gameObject.transform.Find("triangulo DER").GetComponent<SpriteRenderer>().color = color;
        this.gameObject.transform.Find("triangulo IZQ").GetComponent<SpriteRenderer>().color = color;
        this.gameObject.transform.Find("box").GetComponent<SpriteRenderer>().color = color;
    }

    public Color SeleccionDeColor(int n)
    {
        //devuelve el color segun "n". El parametro indica que skin esta seleccionada.

        switch (n)
        {
            case 0:
                colorJugador.r = 0;
                colorJugador.g = 0.7696705f;
                colorJugador.b = 1;
                break;
            case 1:
                colorJugador.r = 0.490566f;
                colorJugador.g = 0.01619794f;
                colorJugador.b = 0.06702313f;
                break;
            case 2:
                colorJugador.r = 0.9245283f;
                colorJugador.g = 0.2224101f;
                colorJugador.b = 0.8873301f;
                break;
            case 3:
                colorJugador.r = 0.01568627f;
                colorJugador.g = 1;
                colorJugador.b = 0;
                break;
            case 4:
                colorJugador.r = 0.6981f;
                colorJugador.g = 0.1855f;
                colorJugador.b = 0f;
                break;
            default:
                colorJugador = Color.black;
                break;
        }


        colorJugador.a = 1f;
        return colorJugador;
    }
}
