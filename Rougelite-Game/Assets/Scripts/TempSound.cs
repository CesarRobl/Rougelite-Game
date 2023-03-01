using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSound : MonoBehaviour
{
    public static TempSound soundtemp;

    public AudioSource[] tempstorage;
    public AudioClip[] clipstorage;

    void Start()
    {
        soundtemp = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
