using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colaManoController : MonoBehaviour
{

    public bool encendido;

    private AnimatorClipInfo[] m_CurrentClipInfo;
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        encendido = false;
        animator = this.transform.root.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("pegandoGeneral"))
        {
            if (encendido == false)
            {
				GetComponent<TrailRenderer>().Clear();
            }

            encendido = true;
        }
        else
        {
            encendido = false;

        }
        GetComponent<TrailRenderer>().enabled = encendido;

    }


}
