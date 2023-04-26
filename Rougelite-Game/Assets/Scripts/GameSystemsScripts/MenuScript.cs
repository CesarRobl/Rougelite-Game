using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public static MenuScript menu;
    public static float lastvolumefloat;
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject crosshairCheck, loadscreen;
    
     public Slider volumeslider;
     public Slider musicSlider;
    private bool opensettings;
    public bool play;
    void Start()
    {
        Time.timeScale = 1;
        menu = this;
        VolumeSliders();
        crosshairCheck.SetActive(GMController.showcrosshair);
    }

    // Update is called once per frame
    void Update()
    {
        
        GMController.volume = volumeslider.value;
        GMController.MusicVolume = musicSlider.value;
        if(play)StartCoroutine(PlayGameScene());
    }

    void VolumeSliders()
    {
        if (GMController.volume != 0) volumeslider.value = GMController.volume;
        else volumeslider.value = 1;
        if (GMController.MusicVolume != 0) musicSlider.value = GMController.MusicVolume;
        else musicSlider.value = 1;
    }
    public void PlayMainScene()
    {
        play = true;
    }

    public void ShowSettings()
    {
        if (!opensettings)
        {
            opensettings = true;
            buttons.SetActive(false);
            settings.SetActive(opensettings);
            return;
        }

        else
        {
            opensettings = false;
            buttons.SetActive(true);
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

     IEnumerator PlayGameScene()
    {
        
        loadscreen.SetActive(true);
       
            loadscreen.GetComponent<RawImage>().color += new Color(0, 0, 0, GMController.fadespeed * Time.deltaTime);
         
        yield return new WaitForSeconds(.85f);
        SceneManager.LoadScene("Intro");
    }
}
