using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Healthbar : MonoBehaviour
{
    public int health;
    public int NumOfHearts;
    
    
    public Image[] Burgers;
    public Sprite FullBurger;
    public Sprite HalfBurger;
    public Sprite EmptyBurger;

    private void Update()
    {
        if (health > NumOfHearts)
        {
            health = NumOfHearts;
        }

        for (int i = 0; i < Burgers.Length; i++)
        {
            Debug.Log((i  * 2) + 1);
           
             if ((i * 2) + 1 == health )
            {
                Burgers[i].sprite = HalfBurger;
            }
             
             else if (i * 2 < health )
             {
                 Burgers[i].sprite = FullBurger;
             }
            
            else
            {
                Burgers[i].sprite = EmptyBurger;
            }

            if (i < NumOfHearts)
            {
                Burgers[i].enabled = true;
            }
            else
            {
                Burgers[i].enabled = false;
            }
        }
    }
}
