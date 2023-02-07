using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public bool playerin;
    public bool bossroom;
    [SerializeField] private bool hidedoor;
    [SerializeField] private DoorScript[] door;
    [SerializeField] private GameObject[] walls;

    private void Awake()
    {
        Invoke("Addlist",.2f);
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }

    void Addlist()
    {
        GMController.gm.rc.Add(this);
    }

    // private void OnTriggerEnter2D(Collider2D col)
    // {
    //     TempPlayer tp = col.gameObject.GetComponent<TempPlayer>();
    //     if (tp != null) playerin = true;
    // }
    //
    // private void OnTriggerExit2D(Collider2D col)
    // {
    //     TempPlayer tp = col.gameObject.GetComponent<TempPlayer>();
    //     if (tp != null) playerin = false;
    // }
}
