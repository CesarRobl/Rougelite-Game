using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour
{
    [HideInInspector] public int powerInt;
    [SerializeField] private LootList item;
    [SerializeField] private bool itemSelect;
    void Awake()
    {
        if(itemSelect) SelectItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SelectItem()
    {
        powerInt = item.idNum;
        GetComponent<SpriteRenderer>().sprite = item.sprite;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
      

        if (powerInt != 4 || powerInt != 5)
        {
             if (GMController.gm.ui.health.health < 6 & col.gameObject.CompareTag("TestPlayer"))
            {
                GMController.gm.id.PowerUp(powerInt);
                Destroy(gameObject);
            }
        }
        
        else if (col.gameObject.CompareTag("TestPlayer"))
        {
            GMController.gm.id.PowerUp(powerInt);
            Destroy(gameObject);
        }
    }

    
}
