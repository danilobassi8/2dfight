using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Kunai_Controller : MonoBehaviour
{



    public float velocidadKunai;
    public float aceleracionGravedad;
    public float maxDeltaX;
    public float maxDeltaY;
    public Sound[] sounds;
    public GameObject Objecto_Fundador;
    public Vector3 direccionATirar;
    public float probabilidadDeMantenerseSano;
    public float factorAmortiguamiento;



    private Vector3 gravedad, velocidad;
    private float deltaX, deltaY;
    private bool kunaiRoto;


    private bool lanzando;
    private Vector3 posicionFinal;
    private Vector3 velocidadPostChoque;
    private bool bandera1;
    private float x, y;
    private float randomDirection;

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

    void Start()
    {
        float r = UnityEngine.Random.Range(0f, 1f);
        if (r < 0.5) //se usa solamente si el kunai se rompe
        {
            randomDirection = 1;
        }
        else
        {
            randomDirection = -1;
        }

        bandera1 = true;
        direccionATirar = this.Objecto_Fundador.GetComponent<Transform>().Find("KunaiSpawner").GetComponent<Kunai_Spawner_Controller>().direccionATirar;

        deltaX = UnityEngine.Random.Range(0, maxDeltaX);
        deltaY = UnityEngine.Random.Range(-maxDeltaY, maxDeltaY);

        gravedad = new Vector3(0, aceleracionGravedad, 0);

        if (direccionATirar.x != 0 && direccionATirar.y != 0)
            velocidad = new Vector3((velocidadKunai + deltaX) * direccionATirar.x, (velocidadKunai + deltaY) * direccionATirar.y, 0);
        else
        {
            if (Objecto_Fundador.transform.localScale.x < 0) // si mira para la izq
            {
                velocidad = new Vector3((velocidadKunai + deltaX) * -1, (velocidadKunai + deltaY) * direccionATirar.y, 0);
            }
            else
            {
                velocidad = new Vector3((velocidadKunai + deltaX) * 1, (velocidadKunai + deltaY) * direccionATirar.y, 0);
            }
        }


        //reproduce sonido.
        this.PlaySound("Kunai" + UnityEngine.Random.Range(1, 5).ToString());


        lanzando = true;

        Destroy(this.gameObject, 4f);
    }


    void Update()
    {


        if (lanzando)
        {
            // Actualiza la velocidad usando leyes de newton.
            velocidad += gravedad * Time.deltaTime;

            // Actualiza posicion.
            transform.position += velocidad * Time.deltaTime;

            //acomoda la rotacion del kunai.
            var rotationVector = transform.rotation.eulerAngles;
            if (velocidad.y > 0)
            {
                rotationVector.z = Vector3.Angle(velocidad, new Vector3(1, 0, 0));
            }
            else
            {
                rotationVector.z = -Vector3.Angle(velocidad, new Vector3(1, 0, 0));
            }

            transform.rotation = Quaternion.Euler(rotationVector);
        }
        else if (kunaiRoto == false)
        {
            this.transform.position = posicionFinal;
        }
        else // cuando el kunai se rompe.
        {
            if (bandera1)//primera vez que entra en la condicion.
            {
                PlaySound("sonidoKunaiRoto_" + UnityEngine.Random.Range(0, 3).ToString());
                Debug.Log("deberia repor");
                bandera1 = false;

            }
            else
            {

                this.transform.Rotate(0, 0, UnityEngine.Random.Range(12, 22));                          //la hace rotar.
                x = randomDirection * UnityEngine.Random.Range(0f, 0.8f) * factorAmortiguamiento;
                y = y - aceleracionGravedad * Time.deltaTime * factorAmortiguamiento;
                transform.position = new Vector3(transform.position.x - x, transform.position.y - y, transform.position.z);
            }

        }

    }


    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.clip.name == name);
        s.source.PlayOneShot(s.clip, 1);
    }



    void OnTriggerEnter2D(Collider2D col)
    {
        string padre = saberPadre(col.gameObject);
        if (padre == Objecto_Fundador.name)
        {
            //que pasa si choca con el padre.
        }
        else
        {
            if (col.gameObject.tag != "Player")
            {
                lanzando = false;

                if (UnityEngine.Random.Range(0f, 1f) > probabilidadDeMantenerseSano)
                {
                    KunaiSeRompe();
                }
                else
                {
                    posicionFinal = this.transform.position;
                }
            }
            else
            {

                // aca se va a rellenar lo que pasa cuando choque con algun personaje que no sea el padre.


            }
        }


    }

    public string saberPadre(GameObject objeto)
    {
        return objeto.transform.root.gameObject.name;
    }

    public void KunaiSeRompe()
    {
        kunaiRoto = true;
        /*
        Activa el booleano de arriba y despues permite que se realicen cosas desde el Update();
        ver comentario "Cuando se rompe".        
        */
    }
}
