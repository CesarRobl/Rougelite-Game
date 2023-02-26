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
    public Sprite EmptyBurger;

    private void Update()
    {
        if (health > NumOfHearts)
        {
            health = NumOfHearts;
        }

        for (int i = 0; i < Burgers.Length; i++)
        {
            if (i < health)
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
