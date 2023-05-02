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
    public GameObject deathAni,retryAni;
    [HideInInspector] public bool attacking,dying,stopAni;
    private bool stop;
    private float spriteTimer = .03f, setTimer;
  
    void Start()
    {
        video = GetComponentInChildren<VideoLists>();
        setTimer = spriteTimer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IFrameEffect()
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
       
         
         Cursor.visible = true;
        
         for (int i = 0; i < GMController.gm.ui.deathButton.Length; i++)
         {
             GMController.gm.ui.deathButton[i].gameObject.SetActive(true);
             GMController.gm.ui.deathButton[i].color += new Color(0, 0, 0, GMController.fadespeed * Time.deltaTime);
         }
         StartCoroutine(BurgerDeath());
         
    }

    IEnumerator BurgerDeath()
    {
        
        deathAni.SetActive(true);
        yield return new WaitForSeconds(.1f);
        stopAni = true;
    }

    public IEnumerator BurgerRetry()
    {
        deathAni.SetActive(false);
        retryAni.SetActive(true);
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

    public IEnumerator IframeEffect(SpriteRenderer sprite)
    {

        float delay = .6f;
            sprite.color = new Color(.5f, .5f, .5f, .3f);
            yield return new WaitForSeconds(delay);
            sprite.color = new Color(1f, 1f, 1f, .3f);  
            yield return new WaitForSeconds(delay);
            sprite.color = new Color(.5f, .5f, .5f, .3f);
            yield return new WaitForSeconds(delay);
            sprite.color = new Color(1f, 1f, 1f, .3f);  
            yield return new WaitForSeconds(delay);
            sprite.color = new Color(.5f, .5f, .5f, .3f);
            yield return new WaitForSeconds(delay);
            sprite.color = new Color(1f, 1f, 1f, .3f);  
            yield return new WaitForSeconds(delay);
    }
    
}
