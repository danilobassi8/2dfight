using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo_luz_script : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<Light>().color = this.transform.parent.GetComponent<escudo_controller>().colorGeneral;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.parent.position.x, this.transform.parent.position.y, 40);
    }
}
