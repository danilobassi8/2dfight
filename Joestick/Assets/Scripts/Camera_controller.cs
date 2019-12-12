using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_controller : MonoBehaviour
{

    public GameObject objeto_A_Seguir;
    private Vector3 vectorSeguir;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        vectorSeguir = objeto_A_Seguir.transform.position;
		vectorSeguir.z = -200;
        vectorSeguir.y = vectorSeguir.y + 3;
		this.transform.position = vectorSeguir;
    }
}
