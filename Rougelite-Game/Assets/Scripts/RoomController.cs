using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public bool playerin;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        TempPlayer tp = col.gameObject.GetComponent<TempPlayer>();
        if (tp != null) playerin = true;
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        TempPlayer tp = col.gameObject.GetComponent<TempPlayer>();
        if (tp != null) playerin = false;
    }
}
