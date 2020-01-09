using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chidori_luz_script : MonoBehaviour
{

    void Start()
    {
        this.gameObject.GetComponent<Light>().color = this.transform.parent.GetComponent<Chidori_sequirPlayer>().color;
    }

    void Update()
    {
        Vector3 pos = new Vector3(this.transform.parent.position.x, this.transform.parent.position.y, 40);
        this.transform.position = pos;
    }
}
