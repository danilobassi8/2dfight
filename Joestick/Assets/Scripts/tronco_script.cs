using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class tronco_script : MonoBehaviour
{

    public bool destruir = false;
    public GameObject prefabHumo;


    public Sound[] sounds;

    private float RotacionInicial;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volumen;
            s.source.pitch = s.pitch;
        }
        PlaySound("substitucion" + UnityEngine.Random.Range(0, 2).ToString());

    }
    void Start()
    {
        this.gameObject.name = "tronco";
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

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.clip.name == name);
        s.source.PlayOneShot(s.clip, 1);
    }
}
