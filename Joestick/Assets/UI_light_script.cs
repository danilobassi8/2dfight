using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_light_script : MonoBehaviour
{




    void Update()
    {
        this.gameObject.GetComponent<Light>().color = this.gameObject.transform.parent.GetComponent<selecter_script>().colorJugador;
    }
}
