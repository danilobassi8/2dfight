using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_efectoGeneral_script : MonoBehaviour
{

    public Color color;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.color = this.transform.parent.gameObject.GetComponent<selecter_script>().colorJugador;
        color.a = 1f;
    }
}
