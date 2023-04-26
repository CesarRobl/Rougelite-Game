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

            case 4:
            {
               BigHealth();
                break;
            }
            
            case 5:
            {
                SmallHealth();
                break;
            }
        }
    }

    IEnumerator DrinkPower()
    {
        GMController.gm.temp.speed = 30;
        yield return new WaitForSeconds(1.4f);
        Debug.Log("Stop Running");
        GMController.gm.temp.speed = 20;
    }

    void SmallHealth()
    {
        SoundControl.Soundcntrl.CharAS.PlayOneShot(TempSound.soundtemp.healthUp);
        GMController.gm.ui.health.health++;
    }

    void BigHealth()
    {
        SoundControl.Soundcntrl.CharAS.PlayOneShot(TempSound.soundtemp.healthUp);
        GMController.gm.ui.health.health += 2;
    }
    
    
}
