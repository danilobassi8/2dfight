using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Kunai_Controller : MonoBehaviour
{



    public float velocidadKunai;
    public Vector3 direccionIR;

    public float aceleracionGravedad;
    public float maxDeltaX;
    public float maxDeltaY;
    public Sound[] sounds;
    public GameObject Objecto_Fundador;

    private Vector3 gravedad, velocidad;
    private float deltaX, deltaY;


    private bool lanzando;
    private Vector3 posicionFinal;


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
        deltaX = UnityEngine.Random.Range(0, maxDeltaX);
        deltaY = UnityEngine.Random.Range(-maxDeltaY, maxDeltaY);
        gravedad = new Vector3(0, aceleracionGravedad, 0);
        velocidad = new Vector3((velocidadKunai + deltaX)*direccionIR.x, (velocidadKunai + deltaY)*direccionIR.y, 0);

        //reproduce sonido.
        this.PlaySound("Kunai" + UnityEngine.Random.Range(1, 5).ToString());


        lanzando = true;
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
        else
        {
            this.transform.position = posicionFinal;
        }

    }


    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.clip.name == name);
        s.source.PlayOneShot(s.clip, 1);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Player")
        {
            lanzando = false;
            posicionFinal = this.transform.position;
        }
        else
        {
            string padre = saberPadre(col.gameObject);
            if (padre == Objecto_Fundador.name)
            {

               
            }
            else // aca se va a rellenar lo que pasa cuando choque con algun personaje que no sea el padre.
            {
                //
            }

        }
    }
  

    public string saberPadre(GameObject objeto)
    {
        string s = "test";
        s = objeto.name;

        if (s == "Player1" || s == "Player2" || s == "Player3" || s == "Player4")
        {
            return s;
        }
        else
        {
            while (s != "Player1" || s != "Player2" || s != "Player3" || s != "Player4")
            {
                return saberPadre(objeto.transform.parent.gameObject);
            }
        }
        return s;
    }
}
