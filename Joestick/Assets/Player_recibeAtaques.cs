using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_recibeAtaques : MonoBehaviour
{

    private Player_Ataques scriptAtaques;
    public bool siendoChidoriado;
    private string nombreAnimacionActual;

    private bool PlayerLockeado;
    public GameObject PlayerMeTaPegando;
    public float clock;
    public float tiempoEspera = 0.1f;

    void Start()
    {
        scriptAtaques = this.gameObject.GetComponent<Player_Ataques>(); //referencia al script de ataques.

        PlayerLockeado = false;
        siendoChidoriado = false;

        clock = tiempoEspera;
    }

    void Update()
    {

        if (siendoChidoriado)
        {
            this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            Invoke("PonerRigidbodyEnDynamic", 1.54f);

            PlayerMeTaPegando.GetComponent<Animator>().SetTrigger("chidori_HIT");
            this.gameObject.GetComponent<Animator>().SetTrigger("chidori_HITED");

            siendoChidoriado = false;
        }


        //tengo que seguir desde acá. activar animaciones y todo eso.
        // poner un actuador (bandera) o algo cuando se active por primera vez el "Siendo chidoriado" y a partir de ahí activar animaciones y jugar con los rigidbody 


    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.root != null)
        {
            if (col.transform.root.tag == "Player")
            {
                //esto es si recive el ataque del chidori de otro Player.
                if (col.transform.root.GetComponent<Player_Ataques>().doingChidori == true && scriptAtaques.doingChidori == false && scriptAtaques.doingEscudo == false && siendoChidoriado == false)
                {
                    PlayerMeTaPegando = col.transform.root.gameObject;
                    siendoChidoriado = true;
                }
            }

        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.root != null)
        {
            if (col.transform.root.tag == "Player" && siendoChidoriado)
            {
                if (col.transform.root.name == PlayerMeTaPegando.name)
                {
                    siendoChidoriado = false;
                    PlayerMeTaPegando = null;
                }
            }
        }
    }

    private void PonerRigidbodyEnDynamic() // ESTO METODO SOLO DEBE SER INVOCADO PARA TERMINAR LA ANIMACION DEL GOLPE DEL CHIDORI. PARA NADA MAS.
    {
        //pongo el rigidbody en Dynamic.
        this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        //le cancelo el chidori
        PlayerMeTaPegando.GetComponent<Player_Ataques>().contadorChidori = 0;
    }



    /* -- ASI ERA ANTES.

void Update()
    {


        nombreAnimacionActual = scriptAtaques.nombreAnimacionActual;

        if (siendoChidoriado == true)
        {
            if (clock <= 0)
            {
                if (nombreAnimacionActual != "Player_chidori_HITED")
                {
                    siendoChidoriado = false;
                }
            }
            else
            {

                clock -= Time.deltaTime;
            }

        }


    }



    void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.root != null)
            {


                if (col.transform.root.tag == "Player")
                {
                    //esto es si recive el ataque del chidori de otro Player.
                    if (col.transform.root.GetComponent<Player_Ataques>().doingChidori == true && scriptAtaques.doingChidori == false && scriptAtaques.doingEscudo == false && siendoChidoriado == false)
                    {
                        siendoChidoriado = true;

                        Debug.Log("ENTRE INT");
                        col.transform.root.GetComponent<Animator>().SetTrigger("chidori_HIT");
                        this.gameObject.GetComponent<Animator>().SetTrigger("chidori_HITED");
                    }
                }

            }

    */







}
