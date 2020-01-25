using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_recibeAtaques : MonoBehaviour
{

    private Player_Ataques scriptAtaques;
    public bool siendoChidoriado;
    private string nombreAnimacionActual;

    void Start()
    {
        scriptAtaques = this.gameObject.GetComponent<Player_Ataques>(); //referencia al script de ataques.


        siendoChidoriado = false;
    }

    void Update()
    {
        nombreAnimacionActual = scriptAtaques.nombreAnimacionActual;

        if (siendoChidoriado == true && nombreAnimacionActual == "New Animation")//////
            siendoChidoriado = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.root != null)
        {


            if (col.transform.root.tag == "Player")
            {
                //esto es si recive el ataque del chidori de otro Player.
                if (col.transform.root.GetComponent<Player_Ataques>().doingChidori == true && scriptAtaques.doingChidori == false && scriptAtaques.doingEscudo == false && siendoChidoriado == false)
                {
                    siendoChidoriado = true;
                    Debug.Log("afectado");
                    col.transform.root.GetComponent<Animator>().SetTrigger("chidori_HIT");
                    this.gameObject.GetComponent<Animator>().SetTrigger("chidori_HITED");
                }
            }

        }


    }


}
