using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagearHijosScript : MonoBehaviour
{
    public string TagYLayer;
    public int capaZ;
    public int MargenDeAcomodacion;

    void Awake()
    {
        FuncionRecursiva(transform, TagYLayer);

    }
    void Start()
    {
        AcomodaHijos(transform, capaZ);

        /*
        if (trans.gameObject.transform.position.z >= capaZ - MargenDeAcomodacion && trans.gameObject.transform.position.z <= capaZ + MargenDeAcomodacion) //doy un margen de 5
                {
                    // si no estan dentro de esta condicion con este margen. deben ser reposicionadas.
                    //eso se hace en el Else.
                }
                else
                {
                    trans.gameObject.transform.position = new Vector3(trans.gameObject.transform.position.x, trans.gameObject.transform.position.y, capaZ); // acomoda la posicion en Z

                }
        */
    }
    void FuncionRecursiva(Transform trans, string tag)
    {
        trans.gameObject.tag = tag; //acomoda el tag
        trans.gameObject.layer = LayerMask.NameToLayer(tag); //acomoda la Layer
        if (trans.childCount > 0)
            foreach (Transform t in trans)
                FuncionRecursiva(t, tag);
    }
    void AcomodaHijos(Transform trans, int capaZ)
    {

        if (trans.parent != null && trans.parent.name == "EXCEPCIONES")
        {
            // si no estan dentro de esta condicion con este margen. deben ser reposicionadas.
            //eso se hace en el Else.
        }
        else
        {
            trans.position = new Vector3(trans.position.x, trans.position.y, capaZ); // acomoda la posicion en z
        }

        if (trans.childCount > 0)
            foreach (Transform t in trans)
                AcomodaHijos(t, capaZ);

    }


}

/*

if (trans.position.z >= capaZ - MargenDeAcomodacion && trans.position.z <= capaZ + MargenDeAcomodacion) //doy un margen de 5
        {
            // si no estan dentro de esta condicion con este margen. deben ser reposicionadas.
            //eso se hace en el Else.
        }
        else
        {
            trans.position = new Vector3(trans.position.x, trans.position.y, capaZ); // acomoda la posicion en Z

        }
		
		*/
