using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI PlayerHP;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHP.text = "HP:" + " " + GMController.gm.playerhealth;
    }
}
