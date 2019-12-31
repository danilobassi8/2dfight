using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class InputController : MonoBehaviour
{
    public _Joestick Joestick1, Joestick2;
    // variables para recuperar en otros scripts.




    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Check_Botones();
        Check_Analogic();

    }

    public void Check_Botones()
    {
        if (Input.GetButton("2Button"))
        {
            Joestick1.b2 = true;
        }
        else
        {
            Joestick1.b2 = false;
        }
        if (Input.GetButton("1Button")) //no me anda el boton del joestick, pero funciona xd.
        {
            Joestick1.b1 = true;
        }
        else
        {
            Joestick1.b1 = false;
        }
        if (Input.GetButton("3Button"))
        {
            Joestick1.b3 = true;
        }
        else
        {
            Joestick1.b3 = false;
        }
        if (Input.GetButton("4Button"))
        {
            Joestick1.b4 = true;
        }
        else
        {
            Joestick1.b4 = false;
        }
        if (Input.GetButton("triggerLT"))
        {
            Joestick1.LT = true;
        }
        else
        {
            Joestick1.LT = false;
        }
        if (Input.GetButton("triggerLB"))
        {
            Joestick1.LB = true;
        }
        else
        {
            Joestick1.LB = false;
        }
        if (Input.GetButton("triggerRT"))
        {
            Joestick1.RT = true;
        }
        else
        {
            Joestick1.RT = false;
        }
        if (Input.GetButton("triggerRB"))
        {
            Joestick1.RB = true;
        }
        else
        {
            Joestick1.RB = false;
        }
        //Flechas
        if (Input.GetAxisRaw("flechasHorizontal") == -1)
        {
            Joestick1.fizquierda = true;
        }
        else
        {
            Joestick1.fizquierda = false;
        }
        if (Input.GetAxisRaw("flechasHorizontal") == 1)
        {
            Joestick1.fderecha = true;
        }
        else
        {
            Joestick1.fderecha = false;
        }
        if (Input.GetAxisRaw("flechasVertical") == -1)
        {
            Joestick1.fabajo = true;
        }
        else
        {
            Joestick1.fabajo = false;
        }
        if (Input.GetAxisRaw("flechasVertical") == 1)
        {
            Joestick1.farriba = true;
        }
        else
        {
            Joestick1.farriba = false;
        }
       
    }

    public void Check_Analogic()
    {
        //Obtengo direccion de la rueda izquierda.
        Joestick1.direccionJoestickIzquierdo = Vector3.zero;
        Joestick1.direccionJoestickIzquierdo.x = (float)Math.Round(Input.GetAxisRaw("Analog_izq_horizontal"), 2);
        Joestick1.direccionJoestickIzquierdo.y = (float)Math.Round(-Input.GetAxisRaw("Analog_izq_vertical"), 2);

        //Obtengo direccion de la rueda derecha.
        Joestick1.direccionJoestickDerecho = Vector3.zero;
        Joestick1.direccionJoestickDerecho.x = (float)Math.Round(Input.GetAxisRaw("Analog_der_horizontal"), 2);
        Joestick1.direccionJoestickDerecho.y = (float)Math.Round(-Input.GetAxisRaw("Analog_der_vertical"), 2);

    }


}

[System.Serializable]
public class _Joestick
{
    public bool b1, b2, b3, b4, farriba, fabajo, fizquierda, fderecha, LT, LB, RB, RT, Start;
    public Vector3 direccionJoestickIzquierdo, direccionJoestickDerecho;
}