using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSound : MonoBehaviour
{
    public static TempSound soundtemp;

    public AudioSource[] tempstorage;
    public AudioClip[] clipstorage;
    public AudioClip[] swordclips;
    public AudioSource talkingsound;

    void Start()
    {
        soundtemp = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Choose 3 numbers to randomly change the pitch of the audio source
    /// </summary>
    /// <param name="source"></param>
    /// <param name="ran"></param>
    public void RandomChangePitch(AudioSource source,float[] ran)
    {
        int pitch = Random.Range(0, ran.Length);
        source.pitch = ran[pitch];
    }
    
    public void ChangePitch(AudioSource source,float[] ran, int pitch)
    {
        
        source.pitch = ran[pitch];
    }
}
