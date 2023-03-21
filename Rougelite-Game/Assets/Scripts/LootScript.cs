using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour
{
    [HideInInspector] public int powerInt;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // if(col.gameObject.CompareTag("TestPlayer")) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("TestPlayer"))
        {
            GMController.gm.id.PowerUp(powerInt);
            Destroy(gameObject);
        }
    }
}
