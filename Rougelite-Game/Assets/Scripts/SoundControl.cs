using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    public static SoundControl Soundcntrl;

    public List<AudioClip> Steps;
    public AudioClip Edamaged;
    public AudioClip Chardamaged;
    //public audi

    public List<AudioClip> Tracklist;
    public Dictionary<string, AudioClip> MusicTracks = new Dictionary<string, AudioClip>();

    public AudioSource CharAS;
    public AudioSource MusicAS;
    public AudioSource EnemyAS;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //run this every 3 seconds
    public void PlaySteps()
    {
        int ran = Random.Range(0, Steps.Count);
        CharAS.clip = Steps[ran];
        CharAS.Play();
    }

    public void CharaDamagePlay()
    {
        CharAS.PlayOneShot(Chardamaged);
    }

    public void EDamagePlay()
    {
        EnemyAS.PlayOneShot(Edamaged);
    }

    public void SwitchTracks(AudioClip newtrack)
    {
        MusicAS.Stop();
        MusicAS.clip = newtrack;
        MusicAS.Play();
    }
}
