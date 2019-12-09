using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Sound
{
    public AudioClip clip;

    public string Nombre;

    [Range(0f, 3f)]
    public float volumen;
    [Range(0f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;


    public void Awake()
    {
        if(Nombre == string.Empty)
        {
            Nombre = clip.name;
        }
    }

}


