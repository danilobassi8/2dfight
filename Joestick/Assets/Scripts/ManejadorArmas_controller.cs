using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorArmas_controller : MonoBehaviour
{
    public bool armado;
    public GameObject ArmaActual;

    public float FuerzaDeTiradoDeArma;
    public float Torque;
    public bool HabilitadoTirar = false;

    private bool armadoAntes; //variable para saber cuando es la primera vez que se arma.
    private GameObject ManoASeguir;
    private bool banderaPrimerTiradaArma;
    private GameObject Player;

    void Start()
    {
        banderaPrimerTiradaArma = false;
        Player = this.transform.root.gameObject;
    }

    void Update()
    {
        if (armado)
        {
            ArmaActual.transform.position = ManoASeguir.transform.position;

            if (armadoAntes == false) //primera vez que se arma.
            {
                armadoAntes = true;

                ArmaActual.gameObject.transform.localScale = new Vector3(Mathf.Abs(ArmaActual.gameObject.transform.localScale.x), Mathf.Abs(ArmaActual.gameObject.transform.localScale.y), Mathf.Abs(ArmaActual.gameObject.transform.localScale.z));
                ArmaActual.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                ArmaActual.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                //le cambio el tipo de rigidbody y le anulo el poligon Collider.

            }

            if (Player.GetComponent<Player1_controller>().joestick.RB && banderaPrimerTiradaArma == false && Player.GetComponent<Player_Ataques>().doingEscudo == false && Player.GetComponent<Player_Ataques>().doingChidori == false && Player.GetComponent<Player_Ataques>().doingKunais == false && Player.GetComponent<Player_Ataques>().doingPiñas == false)
            {
                ArmaActual.GetComponent<arma_general_script>().Invocador.GetComponent<Animator>().SetTrigger("TiraArma");
                banderaPrimerTiradaArma = true;
            }
        }
        if (HabilitadoTirar)
        {
            TirarArma();
            HabilitadoTirar = false; // se activa mediante el animador.
            banderaPrimerTiradaArma = false;
        }


    }

    public void Armar(GameObject Arma)
    {
        ArmaActual = Arma;
        armado = true;

        ArmaActual.GetComponent<arma_general_script>().puedeDañar = true;
        ArmaActual.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        ArmaActual.GetComponent<PolygonCollider2D>().enabled = false;
        ManoASeguir = this.gameObject.transform.root.gameObject.transform.Find("cuerpo").gameObject.transform.Find("mano DER").gameObject;
        ArmaActual.transform.position = ManoASeguir.transform.position;
        ArmaActual.transform.SetParent(ManoASeguir.transform);
    }
    public void TirarArma()
    {

        armado = false;
        armadoAntes = false;
        ManoASeguir = null;

        ArmaActual.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        ArmaActual.GetComponent<PolygonCollider2D>().enabled = true;

        //tira el arma.
        ArmaActual.transform.SetParent(null);
        ArmaActual.GetComponent<arma_general_script>().lanzada = true;
        if (ArmaActual.GetComponent<arma_general_script>().Invocador.GetComponent<Player1_controller>().mirandoDerecha)
        {
            ArmaActual.GetComponent<Rigidbody2D>().velocity = new Vector2(FuerzaDeTiradoDeArma, 0);
            ArmaActual.GetComponent<Rigidbody2D>().AddTorque(Torque);
        }
        else
        {
            ArmaActual.GetComponent<Rigidbody2D>().velocity = new Vector2(-FuerzaDeTiradoDeArma, 0);
            ArmaActual.GetComponent<Rigidbody2D>().AddTorque(-Torque);
        }

        ArmaActual.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        Invoke("TiradaLogica", 1f);

    }

    private void TiradaLogica()
    {
        ArmaActual.GetComponent<arma_general_script>().agarrada = false;

    }
}
