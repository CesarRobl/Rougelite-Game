using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI PlayerHP;
    public TextMeshProUGUI TestFrame;
    public Transform[] BlackBorder;
    public Healthbar health;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHP.text = "HP:" + " " + GMController.gm.playerhealth;
        TestFrame.text = "Playerhurt: " + GMController.gm.playerhurt;

        
        
            

    }
}
