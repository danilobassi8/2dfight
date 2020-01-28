using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaGeneralScriptNew : MonoBehaviour
{
    public bool ArmaEquipada;
    public float NoSePuedeEquipar, DañoArma;

	public GameObject ManoASeguir = null;

    void Start()
    {
        this.gameObject.transform.SetParent(null);
		this.gameObject.tag = "Armas";
		NoSePuedeEquipar = 0;

    }

    void Update()
    {
        if (NoSePuedeEquipar > 0)
            NoSePuedeEquipar -= Time.deltaTime;

        if (ArmaEquipada)
            this.gameObject.transform.position = ManoASeguir.transform.position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (ArmaEquipada)
        {
            //si el arma esta equipada, y se detecta una colision. No hace nada. por ahora.
        }
        else
        {
            if (NoSePuedeEquipar <= 0)
            { // acá va si se puede equipar.

                if (col.tag == "Player")
                {
                    if (col.gameObject.transform.root != null)
                    {
                        if (col.gameObject.transform.root.transform.Find("ManejadorArmas").GetComponent<MenajadorArmasControllerNew>().armado == false)
                        {
                            ArmaEquipada = true;
                            col.gameObject.transform.root.transform.Find("ManejadorArmas").GetComponent<MenajadorArmasControllerNew>().Equipar(this.gameObject);
							ManoASeguir = col.gameObject.transform.root.transform.Find("ManejadorArmas").GetComponent<MenajadorArmasControllerNew>().ManoASeguir;
                        }
                        else
                        {
                            return;
                        }
                    }

                }
                else
                {
                    return;
                }

            }
            else
            { // aca va si no se puede equipar.


            }

        }
    }
}
