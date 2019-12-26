using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escudo_controller : MonoBehaviour
{
    public Color colorGeneral;

    // Use this for initialization
    void Start()
    {
        colorGeneral.a = 1f;
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            child.GetComponent<SpriteRenderer>().color = colorGeneral;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
