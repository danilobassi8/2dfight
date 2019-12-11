using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roto_kunai_scrip : MonoBehaviour
{
	public Vector3 vectorFuerza;
	public float factorAmortiguacion;

    void Start()
    {
		this.GetComponent<Rigidbody2D>().AddForce(vectorFuerza*factorAmortiguacion);
    }

    void Update()
    {

    }
}
