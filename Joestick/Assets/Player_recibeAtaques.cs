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
    public float TiempoRigidDinamic, contadorChidoriPostPiña, tamañoParticulasPostPiña;
    private ParticleSystem ps;

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

            Invoke("CancelarChidori", contadorChidoriPostPiña);
            Invoke("PonerRigidbodyEnDynamic", TiempoRigidDinamic);

            PlayerMeTaPegando.GetComponent<Animator>().SetTrigger("chidori_HIT");
            this.gameObject.GetComponent<Animator>().SetTrigger("chidori_HITED");

            siendoChidoriado = false;
        }

        if (this.gameObject.GetComponent<Player_Ataques>().nombreAnimacionActual == "Player_chidori_HITED")
            this.gameObject.GetComponent<Animator>().ResetTrigger("chidori_HITED");






    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.root != null)
        {
            if (col.transform.root.tag == "Player")
            {
                //esto es si recive el ataque del chidori de otro Player.
                if (col.transform.root.GetComponent<Player_Ataques>().doingChidori == true && col.transform.root.GetComponent<Player_Ataques>().contadorChidori <= 5 && scriptAtaques.doingChidori == false && scriptAtaques.doingEscudo == false && siendoChidoriado == false)
                {
                    PlayerMeTaPegando = col.transform.root.gameObject;
                    siendoChidoriado = true;

                    //agrando el tamaño del efecto del chidori.
                    ps = GameObject.Find(PlayerMeTaPegando.name + "_ParticulasChidori").GetComponent<ParticleSystem>();
                    var main = ps.main;
                    main.startSize = tamañoParticulasPostPiña;


                    //le hago dropear el arma al que le pegan. 
                    if (this.gameObject.transform.Find("ManejadorArmas").GetComponent<ManejadorArmas_controller>().ArmaActual != null)
                        this.gameObject.transform.Find("ManejadorArmas").GetComponent<ManejadorArmas_controller>().SueltaArma(); 
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


    }
    private void CancelarChidori()
    {
        //le cancelo el chidori
        PlayerMeTaPegando.GetComponent<Player_Ataques>().contadorChidori = contadorChidoriPostPiña;
        PlayerMeTaPegando.GetComponent<Player_Ataques>().doingChidori = false;
        if (GameObject.Find(PlayerMeTaPegando.name + "_Chidori") != null)
            Destroy(GameObject.Find(PlayerMeTaPegando.name + "_Chidori"));

        //reseteo los triggers.

        PlayerMeTaPegando.GetComponent<Animator>().ResetTrigger("chidori_HIT");
        this.gameObject.GetComponent<Animator>().ResetTrigger("chidori_HITED");
    }

}
