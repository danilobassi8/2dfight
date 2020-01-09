using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skin_Controller : MonoBehaviour
{
    public int SelectedSkin;
    private int SeleccionSkinAnterior; // esto se hace para saber cuando cambia el skin.

    public Skins[] Skins;

    //
    private GameObject cuerpo;


    public void Awake()
    {
        SeleccionSkinAnterior = SelectedSkin;
    }
    void Start()
    {
        if (SelectedSkin >= Skins.Length)
        {
            while (SelectedSkin >= Skins.Length)
            {
                SelectedSkin = SelectedSkin - Skins.Length;
            }
        }
        if (SelectedSkin < 0)
        {
            SelectedSkin = SelectedSkin + Skins.Length;
        }
        CambiaSkin(this.gameObject, SelectedSkin);
    }

    void Update()
    {
        //Esto hace que no te puedas elegir un skin que no existe.
        if (SelectedSkin >= Skins.Length)
        {
            while (SelectedSkin >= Skins.Length)
            {
                SelectedSkin = SelectedSkin - Skins.Length;
            }
        }
        if (SelectedSkin < 0)
        {
            SelectedSkin = SelectedSkin + Skins.Length;
        }//

        //veo si cambio el numero del skin. De esta manera tambien puedo cambiar el skin en tiempo de ejecucion.
        if (SelectedSkin != SeleccionSkinAnterior)
        {
            SeleccionSkinAnterior = SelectedSkin;
            CambiaSkin(this.gameObject, SelectedSkin);
        }


    }

    public void CambiaSkin(GameObject Player, int nroSkin)
    {
        cuerpo = Player.transform.Find("cuerpo").gameObject;

        cuerpo.transform.Find("CARA").GetComponent<SpriteRenderer>().sprite = Skins[nroSkin].cara;
        cuerpo.transform.Find("Cuerpo").GetComponent<SpriteRenderer>().sprite = Skins[nroSkin].cuerpo;
        cuerpo.transform.Find("mano DER").GetComponent<SpriteRenderer>().sprite = Skins[nroSkin].manoDER;
        cuerpo.transform.Find("mano IZQ").GetComponent<SpriteRenderer>().sprite = Skins[nroSkin].manoIZQ;

    }
}
[System.Serializable]
public class Skins
{
    public string Nombre;
    public Sprite cara, cuerpo, manoIZQ, manoDER;
}
