using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public static MenuScript menu;
    public static float lastvolumefloat;
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject crosshairCheck;
     public Slider volumeslider;
    private bool opensettings;
    void Start()
    {
        menu = this;
        if (GMController.volume != 0) volumeslider.value = GMController.volume;
        else volumeslider.value = 1;
        crosshairCheck.SetActive(GMController.showcrosshair);
    }

    // Update is called once per frame
    void Update()
    {
        
        GMController.volume = volumeslider.value;
        
    }

    
    public void PlayMainScene()
    {
        SceneManager.LoadScene("RandomLevel");
    }

    public void ShowSettings()
    {
        if (!opensettings)
        {
            opensettings = true;
            settings.SetActive(opensettings);
            return;
        }

        else
        {
            opensettings = false;
            settings.SetActive(opensettings);
            return;
        }
    }

    public void Crosshair()
    {
        if (!GMController.showcrosshair)
        {
            GMController.showcrosshair = true;
            crosshairCheck.SetActive(GMController.showcrosshair);
            return;
        }

        else
        {
            GMController.showcrosshair = false;
            crosshairCheck.SetActive(GMController.showcrosshair);
            return;
        }
    }
}