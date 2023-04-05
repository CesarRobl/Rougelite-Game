using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Image = UnityEngine.UIElements.Image;

public class AniController : MonoBehaviour
{
    public Animator[] sword;
    public bool anidone;
 
    [SerializeField] private GameObject[] spatula;
    [HideInInspector] private ParticleSystem ps;
    [SerializeField] private ParticleSystem playerDeath;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject[] hideItems, deathButton;
    private VideoLists video;
    public GameObject deathAni;
    [HideInInspector] public bool attacking,dying,stopAni;
    private bool stop;
  
    void Start()
    {
        video = GetComponentInChildren<VideoLists>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator PlayerDeath()
    {
        GMController.gm.playerDead = true;
        
        for (int i = 0; i < hideItems.Length; i++) hideItems[i].SetActive(false);
        GMController.gm.temp.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        deathScreen.SetActive(true);
        yield return new WaitForSeconds(.5f);
        GMController.gm.temp.GetComponent<SpriteRenderer>().color = Color.clear;
         if(!dying)playerDeath.Play();
         dying = true;
         yield return new WaitForSeconds(1f);
       
         gameOver.SetActive(true);
         Cursor.visible = true;
         for (int i = 0; i < GMController.gm.ui.deathButton.Length; i++)
         {
             GMController.gm.ui.deathButton[i].color += new Color(0, 0, 0, GMController.fadespeed * Time.deltaTime);
         }
         StartCoroutine(BurgerDeath());
         
    }

    IEnumerator BurgerDeath()
    {
        video.video.clip = video.clips[0];
        deathAni.GetComponent<RawImage>().color += Color.white;
        video.video.playbackSpeed = 1;
        yield return new WaitUntil(() =>   deathAni.GetComponent<RawImage>().color. a >= 1);
        deathAni.SetActive(true);
        video.video.isLooping = false;
        yield return new WaitForSeconds(.1f);
        stopAni = true;
    }

    public IEnumerator BurgerRetry()
    {
        video.video.clip = video.clips[1];
        yield return new WaitForSeconds(.1f);
    }
    public IEnumerator SpatulaSwipe()
    {
        attacking = true;
        Vector3 currentpos = spatula[0].transform.localPosition;
       
         sword[0].SetTrigger("Swing");
         sword[1].SetTrigger("Swing");
         spatula[1].transform.position = spatula[2].transform.position;
         spatula[1].transform.eulerAngles =  spatula[3].transform.eulerAngles;
         
        
        yield return new WaitForSeconds(.35f);
        spatula[0].transform.localPosition = currentpos;
        attacking = false;
    }
    
}
