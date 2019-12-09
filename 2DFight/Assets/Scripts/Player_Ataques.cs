using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ataques : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject prefabChidori;
    public string nombreAnimacion;
    public float tiempoChidori;

    public _Joestick joestick;
    private bool doingChidori, chidorispawned;
    public float contadorChidori;
    private string PlayerName;
    private bool instanciaChidori;
    Animator animator;
    private GameObject Spawner_de_kunnais;


    AnimatorClipInfo[] m_CurrentClipInfo;


    void Start()
    {
        PlayerName = "Player1";

        animator = GetComponent<Animator>(); ;
        chidorispawned = false;
        rb = GetComponent<Rigidbody2D>();
        joestick = GameObject.Find("Joestick Controller").GetComponent<InputController>().Joestick1;



        contadorChidori = -1;
        Spawner_de_kunnais = this.transform.Find("Kunai_Spawner").gameObject;


    }


    void Update()
    {

        nombreAnimacion = AnimacionActual(animator);

        Encargado_Chidori();
        Encargado_Kunai();

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
    public void Encargado_Chidori()
    {
        if (joestick.b4)
        {
            if (!doingChidori)
            {
                animator.SetBool("chidori_summon", true);
                animator.Play("Player1_chidori");               //primera vez que hace el chidori, se activa la animacion.
                doingChidori = true;
                contadorChidori = tiempoChidori;
            }


        }
        if (nombreAnimacion == "player1_chidori_middle" && chidorispawned == false)
        {

            chidorispawned = true;
            GameObject my_chidori = GameObject.Instantiate(prefabChidori);

            my_chidori.GetComponent<Chidori_sequirPlayer>().test = false;
            my_chidori.name = PlayerName + "_Chidori";
            my_chidori.transform.position = GameObject.Find(PlayerName).GetComponent<Transform>().Find("mano IZQ").transform.position; //creo que se podria sacar. ver
        }
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

    public void Encargado_Kunai()
    {
       if(joestick.direccionJoestickDerecho.x != 0 | joestick.direccionJoestickDerecho.y != 0)
       {
      
       }
    }
}
