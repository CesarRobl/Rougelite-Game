using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageButtonScript : MonoBehaviour
{

    [SerializeField] private List<GameObject> rooms;
    [SerializeField] private GameObject currentroom;
    public int roomnum;
    private bool stop;
    public float timer;
    public Transform roompos;

    // Update is called once per frame
    void Update()
    {
        if (roomnum > rooms.Count) roomnum = 0;
        if (stop)
        {
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                timer = 0;
                stop = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OW");
        if(col.gameObject.CompareTag("TestPlayer") & !stop)
        {
            Destroy(currentroom);
            currentroom = null;
            roomnum++;
            stop = true;
            if (stop)
            {
                currentroom = Instantiate(rooms[roomnum], roompos.position, Quaternion.identity);
                
            }
        }
    }

   
}
