using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_OK_script : MonoBehaviour
{

    void Update()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = this.gameObject.transform.parent.gameObject.GetComponent<selecter_script>().colorJugador;
    }
}
