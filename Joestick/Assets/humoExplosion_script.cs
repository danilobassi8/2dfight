using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humoExplosion_script : MonoBehaviour
{
    public GameObject objetoASeguir;

    // Use this for initialization
    void Start()
    {
        GameObject.Destroy(this.gameObject, 2.1f);
		
    }

    // Update is called once per frame
    void Update()
    {
        if (objetoASeguir != null)
            this.transform.position = new Vector3(objetoASeguir.transform.position.x, objetoASeguir.transform.position.y, objetoASeguir.transform.position.x - 2);
    }
}
