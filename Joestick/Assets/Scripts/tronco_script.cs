using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tronco_script : MonoBehaviour
{

    public bool destruir = false;
    public GameObject prefabHumo;

    private float RotacionInicial;


    void Start()
    {
        this.gameObject.transform.eulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(0, 360));
        GameObject a = Instantiate(prefabHumo) as GameObject;
        a.GetComponent<humoExplosion_script>().objetoASeguir = this.gameObject;
        a.name = "Humo_de_Tronco";
    }

    // Update is called once per frame
    void Update()
    {
        if (destruir)
            GameObject.Destroy(this.gameObject);
    }
}
