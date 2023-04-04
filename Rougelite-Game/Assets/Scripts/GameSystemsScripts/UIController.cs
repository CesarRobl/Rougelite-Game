using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image[] deathButton;
    public TextMeshProUGUI PlayerHP;
    public TextMeshProUGUI TestFrame;
    public Transform[] BlackBorder;
    public Healthbar health;
    public GameObject MenuTab;
    public GameObject[] Cameras,loadscreen;
    public GameObject BossBar;
    [SerializeField] private GameObject tut;
    [SerializeField] private GameObject map;
    private bool buttonPressed,stopAni,changeScene;
    private int sceneNum;
    

    private bool showmenu,stop;
    void Start()
    {
       Invoke("Camera",.1f);
       
    }

    // Update is called once per frame
    void Update()
    {

        if (GMController.gm.loading & !stop) StartCoroutine(FadeScreen());
        ShowMenuTab();
        if(buttonPressed & !stopAni)PlayButtonFade();
        else if(stopAni) SwitchScene();
    }

    void Camera()
    {
        Cameras[1].transform.SetParent(Cameras[0].transform);
    }
     void ShowMenuTab()
    {
       
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!showmenu)
            {
                Time.timeScale = 0;
                showmenu = true;
                MenuTab.SetActive(showmenu);
                return;
            }
            else
            {
                Time.timeScale = 1;
                Cursor.visible = false;
                showmenu = false;
                MenuTab.SetActive(showmenu);
                return;
            }
        }
    }

     public void ShowBossBar(bool bar)
     {
         BossBar.SetActive(bar);
     }

     public void QuitToMenu()
     {
        // MenuScript.menu.volumeslider.value = MenuScript.lastvolumefloat;
         SceneManager.LoadScene("MainMenu");
     }

     public void RemoveTut()
     {
         if (GMController.gm.loading)
         {
             tut.SetActive(false);
             map.SetActive(true);
             Cursor.visible = false;
             GMController.gm.temp.gameObject.GetComponent<BoxCollider2D>().enabled = true;
             GMController.gm.tutdone = true;
             
         }
     }

     public void Restart()
     {
         if (!buttonPressed)
         {
             StartCoroutine(GMController.gm.ani.BurgerRetry());
             sceneNum = 0;
             buttonPressed = true;
         }
     }

     public void Quit()
     {
         if (!buttonPressed)
         {
             sceneNum = 1;
             buttonPressed = true;
         }
          
     }

     void PlayButtonFade()
     {
         StartCoroutine(FadeDeathButton(sceneNum));
     }
     IEnumerator FadeScreen()
     {
         yield return new WaitForSeconds(2f);
         loadscreen[0].GetComponent<RawImage>().color -= new Color(0, 0, 0, GMController.fadespeed * Time.deltaTime);
         // loadscreen[1].GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, GMController.fadespeed * Time.deltaTime);
         loadscreen[2].GetComponent<TextMeshProUGUI>().color -= new Color(0, 0, 0, GMController.fadespeed * Time.deltaTime);
         yield return new WaitForSeconds(.1f);
         if (loadscreen[0].GetComponent<RawImage>().color.a <= 0)
         {
             loadscreen[0].SetActive(false);
             loadscreen[1].SetActive(false);
             stop = true;
         }
     }

     public IEnumerator FadeDeathButton(int over)
     {
         for (int i = 0; i < deathButton.Length; i++)
         {
             
             deathButton[i].color -= new Color(0, 0, 0, GMController.fadespeed * Time.deltaTime);
         }
         yield return new WaitUntil(() => deathButton[0].color.a <= 0);
         stopAni = true;

     }

     void SwitchScene()
     {
         switch (sceneNum)
         {
             case 0:
             {
                 SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                 break;
             }
             case 1:
             {
                 // Only use the quit option when entering the game through the main menu
                 QuitToMenu();
                 break;
             }
         }
         
     }
     
}
