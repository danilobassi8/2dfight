using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ataques : MonoBehaviour
{

    public GameObject prefabChidori;
    public string nombreAnimacion;
    public float tiempoChidori;
    public float tiempoKunais;

    private _Joestick joestick;
    private Animator animator;
    //variables para el chidori
    private bool doingChidori, chidorispawned;
    public float contadorChidori;
    private string PlayerName;
    private bool instanciaChidori;

    //variables para los kunais
    private GameObject kunaiSpawner;
    private bool doingKunais = false;
    public float clockKunais;

    AnimatorClipInfo[] m_CurrentClipInfo;


    void Start()
    {
        PlayerName = "Player1";

        animator = GetComponent<Animator>(); ;
        chidorispawned = false;

        joestick = GameObject.Find("Joestick Controller").GetComponent<InputController>().Joestick1;
        kunaiSpawner = this.transform.Find("KunaiSpawner").gameObject;


        contadorChidori = -1;
        clockKunais = -1;
    }


    void Update()
    {

        nombreAnimacion = AnimacionActual(animator);

        Manejador_Chidori();
        Manejador_Kuanis();

    }

    public string AnimacionActual(Animator anim) // funcion que me devuelve la animacion que se está reproduciendo.
    {
        try
        {
            m_CurrentClipInfo = this.animator.GetCurrentAnimatorClipInfo(0);
            return m_CurrentClipInfo[0].clip.name;
        }
        catch
        {

        }
        return "empty";


    }

    public void Manejador_Chidori()
    {
        if (joestick.b4 && chidorispawned == false && AnimacionActual(animator) != "player1_chidori_middle") //primera vez que hace el chidori. (contemplar cuando se puede o no hacer.)
        {
            doingChidori = true;
            animator.SetBool("chidori_summon", true);
            animator.Play("Player1_chidori");
            contadorChidori = tiempoChidori;
        }
        if (doingChidori && nombreAnimacion == "player1_chidori_middle" && chidorispawned == false)
        {
            chidorispawned = true;
            GameObject my_chidori = GameObject.Instantiate(prefabChidori);

            my_chidori.GetComponent<Chidori_sequirPlayer>().test = false;
            my_chidori.name = PlayerName + "_Chidori";
            my_chidori.transform.position = GameObject.Find(PlayerName).GetComponent<Transform>().Find("mano IZQ").transform.position;


        }
        else
        {
            if (contadorChidori >= 0)
            {
                contadorChidori -= Time.deltaTime;
            }
            else
            {
                GameObject.Destroy(GameObject.Find(PlayerName + "_Chidori"));
                chidorispawned = false;
                doingChidori = false;
                animator.SetBool("chidori_summon", false);
            }

        }



    }

    public void Manejador_Kuanis()
    {
        if (joestick.LT && doingChidori == false && clockKunais < 0)
        {
            doingKunais = true;
            clockKunais = tiempoKunais;
        }
        if (doingKunais && clockKunais >= 0)
        {
            clockKunais -= Time.deltaTime;
            
            animator.SetBool("tirandoKunais", true);
            animator.Play("Player_lanzando");
            kunaiSpawner.GetComponent<Kunai_Spawner_Controller>().puedeTirar = true;

            if (joestick.direccionJoestickDerecho.x != 0 || joestick.direccionJoestickDerecho.y != 0) // si se elije direccion de tirada
            {
                kunaiSpawner.GetComponent<Kunai_Spawner_Controller>().direccionATirar = new Vector3(joestick.direccionJoestickDerecho.x, joestick.direccionJoestickDerecho.y, 0);
            }
            else
            {
                
            }


        }
        else
        {
            doingKunais = false;
            animator.SetBool("tirandoKunais",false);
            
            kunaiSpawner.GetComponent<Kunai_Spawner_Controller>().puedeTirar = false;
        }
    }
}
