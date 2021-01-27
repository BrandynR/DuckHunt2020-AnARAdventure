using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    AudioSource audio;
    public static GunController instance;

    void Awake()
    {
           if(instance==null)
           {
               instance = this;
           } 
    }

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void fireSound()
    {
        audio.Play();
    }
}
