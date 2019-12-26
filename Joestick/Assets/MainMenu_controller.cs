using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_controller : MonoBehaviour
{
    public GameObject camara;
    private Animator camara_anim;

    public void Start()
    {
        camara_anim = camara.GetComponent<Animator>();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("EleccionPersonajes");
    }
    public void Creditos()
    {

    }
    public void Controles()
    {
        camara_anim.SetTrigger("tocontrol");
        Invoke("ActivaControles", 2);
    }

    public void ActivaControles()
    {
        this.transform.Find("CONTROLES").gameObject.SetActive(true);
    }
    public void ControlesToMenu()
    {
        camara_anim.SetTrigger("controlTOmain");
        Invoke("ActivaMenu", 2);
    }
    public void ActivaMenu()
    {
        this.transform.Find("MAIN").gameObject.SetActive(true);
    }


}
