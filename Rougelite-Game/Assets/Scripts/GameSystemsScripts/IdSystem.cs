using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdSystem : MonoBehaviour
{
    [HideInInspector]

    
    void Update()
    {
//        Debug.Log(GMController.gm.temp.speed);
        
    }

   public void PowerUp(int powerInt)
    {
        switch (powerInt)
        {
            case 1:
            {
               // StartCoroutine(DrinkPower());
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

   // IEnumerator DrinkPower()
  // {
   //     GMController.gm.temp.speed = 30f;
    //    yield return new WaitForSeconds(1.2f);
    //    GMController.gm.temp.speed = 20f;
    //}




}
