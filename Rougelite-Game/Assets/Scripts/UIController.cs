using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI PlayerHP;
    public TextMeshProUGUI TestFrame;
    public Transform[] BlackBorder;
    public Healthbar health;
    public GameObject MenuTab;
    public GameObject[] Cameras;

    private bool showmenu;
    void Start()
    {
       Invoke("Camera",.1f);
    }

    // Update is called once per frame
    void Update()
    {
        // PlayerHP.text = "HP:" + " " + GMController.gm.playerhealth;
        // TestFrame.text = "Playerhurt: " + GMController.gm.playerhurt;

        ShowMenuTab();
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

     public void QuitToMenu()
     {
        MenuScript.menu.volumeslider.value = MenuScript.lastvolumefloat;
         SceneManager.LoadScene("MainMenu");
     }
}
