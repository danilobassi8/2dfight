using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ataques : MonoBehaviour
{
   
    public GameObject prefabChidori;
    public string nombreAnimacion;
    public float tiempoChidori;

    private _Joestick joestick;
    private bool doingChidori, chidorispawned;
    public float contadorChidori;
    private string PlayerName;
    private bool instanciaChidori;
    Animator animator;


    AnimatorClipInfo[] m_CurrentClipInfo;


    void Start()
    {
        PlayerName = "Player1";

        animator = GetComponent<Animator>(); ;
        chidorispawned = false;
        
        joestick = GameObject.Find("Joestick Controller").GetComponent<InputController>().Joestick1;



        contadorChidori = -1;

    }


    void Update()
    {

        nombreAnimacion = AnimacionActual(animator);

        Manejador_Chidori();

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
}
