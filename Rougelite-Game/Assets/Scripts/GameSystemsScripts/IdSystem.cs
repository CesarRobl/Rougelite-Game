using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdSystem : MonoBehaviour
{
    [HideInInspector]

    
    void Update()
    {
        PowerUp(1);
    }

   public void PowerUp(int powerInt)
    {
        switch (powerInt)
        {
            case 1:
            {
                DrinkPower();
                break;
            }

            case 2:
            {
                break;
            }

            case 3:
            {
                break;
            }
        }
    }

    void DrinkPower()
    {
        Debug.Log("drink power up");
    }
    
    
}
