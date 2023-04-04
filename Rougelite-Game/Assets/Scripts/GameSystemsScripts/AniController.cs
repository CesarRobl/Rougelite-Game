using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [HideInInspector] public bool attacking,dying,stopAni;
    private bool stop;
  
    void Start()
    {
       
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
        yield return new WaitForSeconds(1f);
        GMController.gm.temp.GetComponent<SpriteRenderer>().color = Color.clear;
         if(!dying)playerDeath.Play();
         dying = true;
         yield return new WaitForSeconds(.5f);
         gameOver.SetActive(true);
         Cursor.visible = true;
         for (int i = 0; i < GMController.gm.ui.deathButton.Length; i++)
         {
             GMController.gm.ui.deathButton[i].color += new Color(0, 0, 0, GMController.fadespeed * Time.deltaTime);
         }

         yield return new WaitUntil(() => GMController.gm.ui.deathButton[1].color.a >= 1);
         stopAni = true;
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
