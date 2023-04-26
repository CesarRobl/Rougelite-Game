using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCutscene : MonoBehaviour
{
    public TextMeshProUGUI[] text;
    public float imageMoveSpeed;
    [SerializeField] private int scene;
    [SerializeField] private GameObject [] scenes;
    private AudioSource audio;
    [SerializeField] private AudioClip[] clips;
    public DialogueSystem talk;
    private int SceneInt;
    private bool startFade, fadeStop;
    private float FadeSpeed = .75f;
    
    
    
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.volume = GMController.MusicVolume;
        StartCoroutine(ChangeMusic());
        StartCoroutine(StartCountDown());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) scene++;
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("RandomLevel");
        if (startFade == false)
        {
            FadeIn();
            StartFade();
            
        }
        else FadeTransition();
       
       
    }

    IEnumerator ChangeMusic()
    {
        yield return new WaitForSeconds(23f);
        audio.Stop();
        audio.PlayOneShot(clips[1]);
    }

    IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(105f);
        SceneManager.LoadScene("RandomLevel");
    }

    void FadeTransition()
    {
        Debug.Log("My scene int is" + SceneInt);
        scenes[SceneInt].GetComponent<SpriteRenderer>().color -= new Color(0, 0,0 ,.3f * Time.deltaTime);
            if (scenes[SceneInt].GetComponent<SpriteRenderer>().color.a <= 0)
            {
                if (SceneInt < 3)
                {
                    SceneInt++;
                    startFade = false;
                }
                
               
            }
        
    }

    void FadeIn()
    {
       
        if (scenes[SceneInt].GetComponent<SpriteRenderer>().color.a <= 1)
        {
            scenes[SceneInt].GetComponent<SpriteRenderer>().color += new Color(0, 0,0 ,.3f * Time.deltaTime);
           
        }   
    }
    void StartFade()
    {
        if (talk.typeTimer <= 2)
        {
          
            startFade = true;
        }
    }
    
    }

