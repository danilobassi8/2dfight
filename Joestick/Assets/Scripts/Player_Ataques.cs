using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Player_Ataques : MonoBehaviour
{

    public GameObject prefabChidori;
    public string nombreAnimacionActual;
    public float tiempoChidori;
    public float tiempoKunais;
    public float HabilitacionKunai;
    public Sound[] sounds;
    public bool PuedeDañar;
    

    private _Joestick joestick;
    private Animator animator;
    private Rigidbody2D rb;
    private GameObject chekeadorPiso;
    //variables para el chidori
    private bool doingChidori, chidorispawned;
    public float contadorChidori;
    private string PlayerName;
    private bool instanciaChidori;
    private bool banderaSonido, banderaPrimerCastChidori;


    //variables para los kunais
    private GameObject kunaiSpawner;
    private bool doingKunais = false;
    private float clockKunais;
    public float clockHabilitacionKunai;

    //variables para los ataques normales.
    private bool banderaPrimerPiña;
    private bool doingPiñas;

    private bool tocandoPiso;

    AnimatorClipInfo[] m_CurrentClipInfo;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volumen;
            s.source.pitch = s.pitch;
        }
    }
    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.clip.name == name);
        s.source.PlayOneShot(s.clip, 1);
    }

    void Start()
    {
        PlayerName = "Player1";

        animator = GetComponent<Animator>(); ;
        chidorispawned = false;

        joestick = GameObject.Find("Joestick Controller").GetComponent<InputController>().Joestick1;
        kunaiSpawner = this.transform.Find("KunaiSpawner").gameObject;
        rb = this.GetComponent<Rigidbody2D>();

        // banderas y constantes iniciales. no tocar.
        contadorChidori = -1;
        clockKunais = -1;
        clockHabilitacionKunai = -1;
        HabilitacionKunai = HabilitacionKunai + tiempoKunais;
        banderaSonido = true;
        banderaPrimerCastChidori = true;
        banderaPrimerPiña = true;
        PuedeDañar = false;




        doingPiñas = false;
        doingChidori = false;
        doingKunais = false;
    }


    void Update()
    {

        nombreAnimacionActual = AnimacionActual(animator);

        Manejador_Chidori();
        Manejador_Kuanis();
        Manejador_AtaquesNormales();
        Manejador_Boludeces();

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
        if (joestick.b1 && chidorispawned == false && nombreAnimacionActual != "player1_chidori_middle" && doingKunais == false && banderaPrimerCastChidori && doingPiñas == false) //primera vez que hace el chidori. (contemplar cuando se puede o no hacer.)
        {
            doingChidori = true;
            banderaPrimerCastChidori = false;

            animator.SetBool("chidori_summon", true);
            animator.Play("Player1_chidori");
            if (banderaSonido)
            {
                PlaySound("manos");
                banderaSonido = false;
            }


            contadorChidori = tiempoChidori;


        }
        if (doingChidori && nombreAnimacionActual == "player1_chidori_middle" && chidorispawned == false)
        {
            chidorispawned = true;
            GameObject my_chidori = GameObject.Instantiate(prefabChidori);

            my_chidori.GetComponent<Chidori_sequirPlayer>().test = false;
            my_chidori.name = PlayerName + "_Chidori";
            my_chidori.transform.position = GameObject.Find(PlayerName).GetComponent<Transform>().Find("cuerpo").GetComponent<Transform>().Find("mano IZQ").transform.position;


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
                banderaSonido = true;
                banderaPrimerCastChidori = true;

                animator.SetBool("chidori_summon", false);
            }

        }



    }

    public void Manejador_Kuanis()
    {
        tocandoPiso = this.transform.Find("ChekeadorPiso").gameObject.GetComponent<chekeadorPiso>().tocandoPiso;

        if (joestick.LT && doingChidori == false && doingPiñas == false && clockKunais < 0 && clockHabilitacionKunai < 0)
        {
            doingKunais = true;
            clockHabilitacionKunai = HabilitacionKunai;
            clockKunais = tiempoKunais;
        }


        if (doingKunais && clockKunais >= 0 && clockHabilitacionKunai > 0)
        {
            clockKunais -= Time.deltaTime;


            if (tocandoPiso)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                if (this.joestick.direccionJoestickIzquierdo == new Vector3(0, 0, 0) && GetComponent<Rigidbody2D>().velocity.y > 0 && joestick.LT == false)
                    rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }


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
            animator.SetBool("tirandoKunais", false);
            clockKunais = -1;
            //le vuelve a afectar la gravedad
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            //

            kunaiSpawner.GetComponent<Kunai_Spawner_Controller>().puedeTirar = false;
        }
        if (clockHabilitacionKunai >= 0)
        {
            clockHabilitacionKunai -= Time.deltaTime;

        }
        else
        {

        }
    }

    void Manejador_AtaquesNormales()
    {
        if (joestick.b4 && doingChidori == false && doingKunais == false && banderaPrimerPiña && doingPiñas)
        {
            animator.SetBool("pegandoGeneral", true);
            banderaPrimerPiña = false;
            doingPiñas = true;
        }
        if (joestick.b4)
        {
            // si esta apretando el boton de pegar.
            if (nombreAnimacionActual == "Player_ataque1")
            {
                
            }
            if (nombreAnimacionActual == "Player_ataque2")
            {

            }
            if (nombreAnimacionActual == "Player_ataque3")
            {

            }
            if (nombreAnimacionActual == "Player_ataque4")
            {

            }
        }

    }

    public void Manejador_Boludeces()
    {
        //new animation se llama la animacion por defecto.. si, mala mía.
        if (joestick.fabajo && doingChidori == false && doingKunais == false)
        {
            if ((nombreAnimacionActual == "New Animation" || nombreAnimacionActual == "Player_idle_YBAILO"))
                animator.SetBool("ybailo", true);
        }
        else
        {
            animator.SetBool("ybailo", false);
        }
    }




}
