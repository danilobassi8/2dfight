using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_fx_script : MonoBehaviour
{

    public GameObject PadreColor;
    private Color color, my_color;
    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        var main = ps.main;
        Color my_color = this.transform.parent.gameObject.GetComponent<UI_efectoGeneral_script>().color;
        my_color.a = 1f;
        main.startColor = my_color;
    }
}
