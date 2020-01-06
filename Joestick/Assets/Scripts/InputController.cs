using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class InputController : MonoBehaviour
{
    public _Joestick Joestick1, Joestick2, Joestick3, Joestick4;
    // variables para recuperar en otros scripts.

    private string identificador;


    void Start()
    {
        Joestick1.numero = 1;   // se utilizan solamente para el identificador.
        Joestick2.numero = 2;
        Joestick3.numero = 3;
        Joestick4.numero = 4;
    }

    void Update()
    {
        Check_Botones(Joestick1);
        Check_Botones(Joestick2);
        Check_Botones(Joestick3);
        Check_Botones(Joestick4);
        


        Check_Analogic(Joestick1);
        Check_Analogic(Joestick2);
        Check_Analogic(Joestick3);
        Check_Analogic(Joestick4);


    }

    public void Check_Botones(_Joestick Joestick)
    {
        identificador = "_j" + Joestick.numero.ToString();

        if (Input.GetButton("2Button" + identificador))
        {
            Joestick.b2 = true;
        }
        else
        {
            Joestick.b2 = false;
        }
        if (Input.GetButton("1Button" + identificador))
        {
            Joestick.b1 = true;
        }
        else
        {
            Joestick.b1 = false;
        }
        if (Input.GetButton("3Button" + identificador))
        {
            Joestick.b3 = true;
        }
        else
        {
            Joestick.b3 = false;
        }
        if (Input.GetButton("4Button" + identificador))
        {
            Joestick.b4 = true;
        }
        else
        {
            Joestick.b4 = false;
        }
        if (Input.GetButton("triggerLT" + identificador))
        {
            Joestick.LT = true;
        }
        else
        {
            Joestick.LT = false;
        }
        if (Input.GetButton("triggerLB" + identificador))
        {
            Joestick.LB = true;
        }
        else
        {
            Joestick.LB = false;
        }
        if (Input.GetButton("triggerRT" + identificador))
        {
            Joestick.RT = true;
        }
        else
        {
            Joestick.RT = false;
        }
        if (Input.GetButton("triggerRB" + identificador))
        {
            Joestick.RB = true;
        }
        else
        {
            Joestick.RB = false;
        }
        //Flechas
        if (Input.GetAxisRaw("flechasHorizontal" + identificador) == -1)
        {
            Joestick.fizquierda = true;
        }
        else
        {
            Joestick.fizquierda = false;
        }
        if (Input.GetAxisRaw("flechasHorizontal" + identificador) == 1)
        {
            Joestick.fderecha = true;
        }
        else
        {
            Joestick.fderecha = false;
        }
        if (Input.GetAxisRaw("flechasVertical" + identificador) == -1)
        {
            Joestick.fabajo = true;
        }
        else
        {
            Joestick.fabajo = false;
        }
        if (Input.GetAxisRaw("flechasVertical" + identificador) == 1)
        {
            Joestick.farriba = true;
        }
        else
        {
            Joestick.farriba = false;
        }
        if (Input.GetAxisRaw("Start" + identificador) == 1)
        {
            Debug.Log("Start");
            Joestick.Start = true;
        }
        else
        {
            Joestick.Start = false;
        }


    }

    public void Check_Analogic(_Joestick Joestick)
    {

        identificador = "_j" + Joestick.numero.ToString();

        //Obtengo direccion de la rueda izquierda.
        Joestick.direccionJoestickIzquierdo = Vector3.zero;
        Joestick.direccionJoestickIzquierdo.x = (float)Math.Round(Input.GetAxisRaw("Analog_izq_horizontal" + identificador), 2);
        Joestick.direccionJoestickIzquierdo.y = (float)Math.Round(-Input.GetAxisRaw("Analog_izq_vertical" + identificador), 2);

        //Obtengo direccion de la rueda derecha.
        Joestick.direccionJoestickDerecho = Vector3.zero;
        Joestick.direccionJoestickDerecho.x = (float)Math.Round(Input.GetAxisRaw("Analog_der_horizontal" + identificador), 2);
        Joestick.direccionJoestickDerecho.y = (float)Math.Round(-Input.GetAxisRaw("Analog_der_vertical" + identificador), 2);

    }


}

[System.Serializable]
public class _Joestick
{
    public int numero;
    public bool b1, b2, b3, b4, farriba, fabajo, fizquierda, fderecha, LT, LB, RB, RT, Start;
    public Vector3 direccionJoestickIzquierdo, direccionJoestickDerecho;
}