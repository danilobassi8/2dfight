using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Player_Ataques : MonoBehaviour
{

    public GameObject prefabChidori;
    public Color colorChidori;
    public string nombreAnimacionActual;
    public float tiempoChidori;
    public float tiempoKunais;
    public float HabilitacionKunai;
    public Sound[] sounds;
    public bool PuedeDañar; //ver
    public float radioAtaquePiñas;



    private _Joestick joestick;
    private Animator animator;
    private Rigidbody2D rb;
    private GameObject chekeadorPiso;
    //variables para el chidori
    public bool doingChidori, chidorispawned;
    public float contadorChidori;
    private string PlayerName;
    private bool instanciaChidori;
    private bool banderaSonido, banderaPrimerCastChidori;


    //variables para los kunais
    private GameObject kunaiSpawner;
    public bool doingKunais = false;
    private float clockKunais;
    public float clockHabilitacionKunai;

    //variables para los ataques normales.
    private bool banderaPrimerPiña;
    public bool doingPiñas;
    private Transform puntoPiñas;

    //variables para la transformacion.
    public int TransformacionesPosibles;
    private bool mirandoDerecha;
    public GameObject prefabTronco;
    public float tiempoEntreTroncos;
    private float clockTronco;
    public GameObject prefabHumo;
    private GameObject CheckTeleport;
    private GameObject p2, p3;
    private GameObject coordenadas;
    private bool excepcionR, banderaPrimerTP;

    //variables para el escudo.
    public bool doingEscudo;
    public GameObject prefabEscudo;
    public bool quietito;
    private Vector3 posicionquieto;
    public float tiempoEntreEscudo, duracionEscudo;
    private float clockescudo, clockEntreEscudos;
    private bool primeravezescudo, primeravezquieto;



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
        PlayerName = this.transform.root.gameObject.name;

        animator = GetComponent<Animator>(); 
        chidorispawned = false;

        string nroPlayer = this.gameObject.name[this.gameObject.name.Length - 1].ToString();
        if (nroPlayer == "1")
        {
            joestick = GameObject.Find("Joestick Controller").GetComponent<InputController>().Joestick1;
        }
        else if (nroPlayer == "2")
        {
            joestick = GameObject.Find("Joestick Controller").GetComponent<InputController>().Joestick2;
        }
        else if (nroPlayer == "3")
        {
            joestick = GameObject.Find("Joestick Controller").GetComponent<InputController>().Joestick3;
        }
        else if (nroPlayer == "4")
        {
            joestick = GameObject.Find("Joestick Controller").GetComponent<InputController>().Joestick4;
        }



        kunaiSpawner = this.transform.Find("KunaiSpawner").gameObject;
        rb = this.GetComponent<Rigidbody2D>();
        puntoPiñas = this.transform.root.gameObject.transform.Find("cuerpo").gameObject.transform.Find("mano DER");
        CheckTeleport = this.gameObject.transform.Find("ChekeadorPiso/CheckTeleport").gameObject;

        // banderas y constantes iniciales. no tocar.
        contadorChidori = -1;
        clockKunais = -1;
        clockHabilitacionKunai = -1;
        clockTronco = -1;
        clockescudo = -1;
        HabilitacionKunai = HabilitacionKunai + tiempoKunais;
        banderaSonido = true;
        banderaPrimerCastChidori = true;
        banderaPrimerPiña = true;
        primeravezescudo = true;
        PuedeDañar = false;
        excepcionR = false;
        banderaPrimerTP = true;
        primeravezquieto = true;
        clockEntreEscudos = tiempoEntreEscudo;




        doingPiñas = false;
        doingChidori = false;
        doingKunais = false;
        doingEscudo = false;
    }


    void Update()
    {

        nombreAnimacionActual = AnimacionActual(animator);

        Manejador_Transformacion();
        Manejador_Escudo();
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
        if (joestick.b1 && chidorispawned == false && nombreAnimacionActual != "player1_chidori_middle" && doingKunais == false && banderaPrimerCastChidori && doingPiñas == false && doingEscudo == false && nombreAnimacionActual != "Player_TiraArma") //primera vez que hace el chidori. (contemplar cuando se puede o no hacer.)
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
            my_chidori.GetComponent<Chidori_sequirPlayer>().color = colorChidori;
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

        if (joestick.LT && doingChidori == false && doingPiñas == false && clockKunais < 0 && clockHabilitacionKunai < 0 && doingEscudo == false && nombreAnimacionActual != "Player_TiraArma")
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
        if (joestick.b4 && doingChidori == false && doingKunais == false && banderaPrimerPiña && doingPiñas == false && doingEscudo == false && nombreAnimacionActual != "Player_TiraArma")
        {
            animator.SetBool("pegandoGeneral", true);
            animator.SetTrigger("pegandoTrigger");
            banderaPrimerPiña = false;
            doingPiñas = true;
        }
        if (joestick.b4 && doingPiñas == true)
        {
            Atacar();

        }
        else
        {
            doingPiñas = false;
            banderaPrimerPiña = true;
            animator.SetBool("pegandoGeneral", false);
        }

    }

    public void Manejador_Boludeces()
    {
        //new animation se llama la animacion por defecto.. si, mala mía.
        if (joestick.fabajo && doingChidori == false && doingKunais == false && doingEscudo == false && nombreAnimacionActual != "Player_TiraArma")
        {
            if ((nombreAnimacionActual == "New Animation" || nombreAnimacionActual == "Player_idle_YBAILO"))
                animator.SetBool("ybailo", true);
        }
        else
        {
            animator.SetBool("ybailo", false);
        }
    }

    public void Atacar()
    {
        Collider2D[] objetosGolpeados = Physics2D.OverlapCircleAll(puntoPiñas.position, radioAtaquePiñas); //traigo todos los objetos tocados
        List<GameObject> enemigosGolpeados = new List<GameObject>();

        //los filtro
        foreach (Collider2D enemigo in objetosGolpeados)
        {
            GameObject rooter = enemigo.transform.root.gameObject; //objeto root
            if (rooter.name != PlayerName)
            {
                if (enemigosGolpeados.Contains(rooter) == false) // si la lista no contiene a ese objeto.
                {
                    enemigosGolpeados.Add(rooter);
                }

            }
        }
        string s = "";
        foreach (GameObject objetos in enemigosGolpeados)
        {
            s = s + objetos.name + " - ";
        }
        Debug.Log("golpeamos a : " + s);
    }

    public void Manejador_Transformacion()
    {
        // sirve para saber si mira o no a la derecha.
        if (joestick.direccionJoestickIzquierdo.x < 0)
        {
            mirandoDerecha = false;

        }
        else if (joestick.direccionJoestickIzquierdo.x > 0)
        {
            mirandoDerecha = true;

        } // hasta aca.


        if (joestick.RT && (joestick.direccionJoestickDerecho.x != 0f || joestick.direccionJoestickDerecho.y != 0f) && doingKunais == false && TransformacionesPosibles > 0 && clockTronco < 0 && banderaPrimerTP && doingEscudo == false)
        {
            banderaPrimerTP = false;
            Vector3 posicionPreTransformacion = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            float x = joestick.direccionJoestickDerecho.x;
            float y = joestick.direccionJoestickDerecho.y;
            if (y < 0)
                y = -y;

            TeleTransporta(x, y);



            //invoca al tronco en el lugar previo a la invocacion.
            GameObject a = Instantiate(prefabTronco) as GameObject;
            a.transform.position = posicionPreTransformacion;
            a.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 1, 0);
            clockTronco = tiempoEntreTroncos;
            banderaPrimerTP = true;
        }

        if (clockTronco >= 0)
            clockTronco -= Time.deltaTime;
    }

    void Manejador_Escudo()
    {
        if (joestick.b2 && doingKunais == false && doingPiñas == false && doingEscudo == false && doingChidori == false && clockescudo < 0 && clockEntreEscudos >= tiempoEntreEscudo && nombreAnimacionActual != "Player_TiraArma")
        {
            doingEscudo = true;
            clockescudo = duracionEscudo;
            animator.SetBool("escudando", true);
        }
        if (doingEscudo == true && nombreAnimacionActual == "player_escudo_on")
        {
            clockescudo -= Time.deltaTime;

            if (primeravezescudo)
            {
                primeravezescudo = false;

                GameObject my_escudo = GameObject.Instantiate(prefabEscudo);
                quietito = true;
                primeravezquieto = true;
                posicionquieto = this.gameObject.transform.position;
                clockEntreEscudos = 0;

                //instancia el escudo.
                my_escudo.GetComponent<escudo_controller>().invocador = this.gameObject;
                my_escudo.transform.position = new Vector3(this.gameObject.transform.position.x, this.transform.position.y, this.transform.position.z - 3);
                my_escudo.GetComponent<escudo_controller>().colorGeneral = colorChidori;
                my_escudo.name = "escudo";
                Destroy(my_escudo, duracionEscudo);
            }
        }
        if (clockescudo < 0 && nombreAnimacionActual == "player_escudo_on")
        {
            doingEscudo = false;
            primeravezescudo = true;
            animator.SetBool("escudando", false);
            quietito = false;
            if (primeravezquieto)
            {
                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                primeravezquieto = false;
            }
        }
        if (quietito)
        {
            this.gameObject.transform.position = posicionquieto;
        }
        if (doingEscudo == false && clockEntreEscudos < tiempoEntreEscudo)
        {
            clockEntreEscudos += Time.deltaTime;
        }
    }

    void TeleTransporta(float x, float y)
    {


        Vector3 posicionTransportarFINAL = new Vector3(0, 0, 0);

        if (mirandoDerecha || excepcionR)
        {
            if (x > 0 && y == 0)
            {
                coordenadas = CheckTeleport.transform.Find("10").gameObject;

                p2 = coordenadas.transform.Find("p2").gameObject;
                p3 = coordenadas.transform.Find("p3").gameObject;

                if (p3.GetComponent<puntoTP_controller>().dentroMapa)
                {
                    posicionTransportarFINAL = p3.transform.position;
                }
                else if (p3.GetComponent<puntoTP_controller>().dentroMapa)
                {
                    posicionTransportarFINAL = p2.transform.position;
                }
                else
                {
                    TeleTransporta(1, 1);
                    return;
                }

            }
            else if (x > 0 && y > 0)
            {
                coordenadas = CheckTeleport.transform.Find("11").gameObject;

                p2 = coordenadas.transform.Find("p2").gameObject;
                p3 = coordenadas.transform.Find("p3").gameObject;

                if (p3.GetComponent<puntoTP_controller>().dentroMapa)
                {
                    posicionTransportarFINAL = p3.transform.position;
                }
                else if (p3.GetComponent<puntoTP_controller>().dentroMapa)
                {
                    posicionTransportarFINAL = p2.transform.position;
                }
                else
                {
                    TeleTransporta(0, 1);
                    return;
                }
            }
            else if (x == 0 && y > 0)
            {
                coordenadas = CheckTeleport.transform.Find("01").gameObject;

                p2 = coordenadas.transform.Find("p2").gameObject;
                p3 = coordenadas.transform.Find("p3").gameObject;

                if (p3.GetComponent<puntoTP_controller>().dentroMapa)
                {
                    posicionTransportarFINAL = p3.transform.position;
                }
                else if (p3.GetComponent<puntoTP_controller>().dentroMapa)
                {
                    posicionTransportarFINAL = p2.transform.position;
                }
                else
                {
                    // no se transporta a ningun lado.
                    return;
                }
            }
            else if (x < 0 && y > 0)
            {
                coordenadas = CheckTeleport.transform.Find("-11").gameObject;

                p2 = coordenadas.transform.Find("p2").gameObject;
                p3 = coordenadas.transform.Find("p3").gameObject;

                if (p3.GetComponent<puntoTP_controller>().dentroMapa)
                {
                    posicionTransportarFINAL = p3.transform.position;
                }
                else if (p3.GetComponent<puntoTP_controller>().dentroMapa)
                {
                    posicionTransportarFINAL = p2.transform.position;
                }
                else
                {
                    TeleTransporta(0, 1);
                    return;
                }
            }
            else if (x < 0 && y == 0)
            {
                coordenadas = CheckTeleport.transform.Find("-10").gameObject;

                p2 = coordenadas.transform.Find("p2").gameObject;
                p3 = coordenadas.transform.Find("p3").gameObject;

                if (p3.GetComponent<puntoTP_controller>().dentroMapa)
                {
                    posicionTransportarFINAL = p3.transform.position;
                }
                else if (p3.GetComponent<puntoTP_controller>().dentroMapa)
                {
                    posicionTransportarFINAL = p2.transform.position;
                }
                else
                {
                    TeleTransporta(-1, 1);
                    return;
                }
            }
            else if (posicionTransportarFINAL == new Vector3(0, 0, 0))
            {
                return;
            }

            //instancia al jugador en la posicion, e invoca humo.
            GameObject a = Instantiate(prefabHumo) as GameObject;
            a.name = PlayerName + "HumoTransportacion";
            a.GetComponent<humoExplosion_script>().objetoASeguir = this.gameObject;
            this.transform.position = posicionTransportarFINAL;

        }
        else // si miraba a la izquierda.
        {
            excepcionR = true;
            TeleTransporta(-x, y);
            excepcionR = false;
            return;
        }
    }

    //Aca estaba el onDrawGrizmos de las piñas. lo borre, hay que reacerlo.

}
