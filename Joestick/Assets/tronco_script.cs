using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tronco_script : MonoBehaviour
{

    public bool destruir = false;

    private float RotacionInicial;


    void Start()
    {
        this.gameObject.transform.eulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(0, 360));
    }

    // Update is called once per frame
    void Update()
    {
        if (destruir)
            GameObject.Destroy(this.gameObject);
    }
}
