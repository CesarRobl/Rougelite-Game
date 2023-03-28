using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdSystem : MonoBehaviour
{
    [HideInInspector]

    
    void Update()
    {
        
    }

   public void PowerUp(int powerInt)
    {
        switch (powerInt)
        {
            case 1:
            {
                StartCoroutine(DrinkPower());
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

    IEnumerator DrinkPower()
    {
        GMController.gm.temp.speed = 30;
        yield return new WaitForSeconds(1.4f);
        GMController.gm.temp.speed = 20;
    }
    
    
}
