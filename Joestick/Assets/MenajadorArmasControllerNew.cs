using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenajadorArmasControllerNew : MonoBehaviour
{
    public bool armado;
    public float FuerzaTiradoArma,FuerzaTiradoY, Torque;
    public GameObject ManoASeguir;
    public bool HabilitadoTirar;
	public float TiempoPostTirada;

    private GameObject ArmaActual;
    private bool primeraEquipada = false;
    private GameObject Player;
    private bool banderaPrimerTiradaArma;


    void Start()
    {
        ManoASeguir = this.transform.root.gameObject.transform.Find("cuerpo/mano DER").gameObject;

        Player = this.transform.root.gameObject;

        banderaPrimerTiradaArma = false;
    }


    void Update()
    {


        if (primeraEquipada)
        {
            //se hace para que el arma se acomode bien. SOLO en la primera equipada.
            primeraEquipada = false;

            ArmaActual.gameObject.transform.localScale = new Vector3(Mathf.Abs(ArmaActual.gameObject.transform.localScale.x), Mathf.Abs(ArmaActual.gameObject.transform.localScale.y), Mathf.Abs(ArmaActual.gameObject.transform.localScale.z));
            ArmaActual.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            ArmaActual.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }


        //Si tira el arma.
        if (armado)
        {
            if (Player.GetComponent<Player1_controller>().joestick.RB && banderaPrimerTiradaArma == false && Player.GetComponent<Player_Ataques>().doingEscudo == false && Player.GetComponent<Player_Ataques>().doingChidori == false && Player.GetComponent<Player_Ataques>().doingKunais == false && Player.GetComponent<Player_Ataques>().doingPiñas == false)
            {
                banderaPrimerTiradaArma = true; //bandera logica para tirar el arma.
                Player.GetComponent<Animator>().SetTrigger("TiraArma");
            }
        }

        if (banderaPrimerTiradaArma) //cuando da la orden de tirar el arma.
        {
            if (HabilitadoTirar) // se activa mediante el animador.
            {
                HabilitadoTirar = false;
                banderaPrimerTiradaArma = false;

                TirarArma();
            }


        }


    }

    public void TirarArma() // por defecto. la tira donde esté mirando el personaje.
    {
        armado = false;

        ArmaActual.GetComponent<ArmaGeneralScriptNew>().NoSePuedeEquipar = TiempoPostTirada;
        ArmaActual.GetComponent<ArmaGeneralScriptNew>().ArmaEquipada = false;
        ArmaActual.GetComponent<ArmaGeneralScriptNew>().ManoASeguir = null;

        ArmaActual.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        ArmaActual.GetComponent<PolygonCollider2D>().enabled = true;

        ArmaActual.transform.SetParent(null);

        if (Player.GetComponent<Player1_controller>().mirandoDerecha)
        {
            ArmaActual.GetComponent<Rigidbody2D>().velocity = new Vector2(FuerzaTiradoArma, FuerzaTiradoY);
            ArmaActual.GetComponent<Rigidbody2D>().AddTorque(Torque);
        }
        else
        {
            ArmaActual.GetComponent<Rigidbody2D>().velocity = new Vector2(-FuerzaTiradoArma, FuerzaTiradoY);
            ArmaActual.GetComponent<Rigidbody2D>().AddTorque(-Torque);
        }

        ArmaActual.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

    }

    public void TirarArma(Vector2 fuerza) // se puede elegir la fuerza donde tirar.
    {

    }



    public void Equipar(GameObject Arma)
    {
        ArmaActual = Arma;
        armado = true;

        ArmaActual.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        ArmaActual.GetComponent<PolygonCollider2D>().enabled = false;

        ArmaActual.transform.position = ManoASeguir.transform.position;
        ArmaActual.transform.SetParent(ManoASeguir.transform);

        primeraEquipada = true;
    }
}
