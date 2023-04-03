using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlashRay : MonoBehaviour
{
   [SerializeField]private List<GameObject> things;
   public bool stop;

   private void OnTriggerEnter2D(Collider2D col)
   {
     
      HurtFunction ow = col.gameObject.GetComponent<HurtFunction>();
      if (ow != null & !stop)
      {
       
         things.Add(col.gameObject);
         HitCheck();
         stop = true;
         if (stop) stop = false;
      }
   }

   void HitCheck()
   {
      GameObject[] thingies = things.ToArray();
      for (int i = 0; i < thingies.Length; i++)
      {
         if(RayCheck(thingies[i].transform, thingies[i].name)) thingies[i].GetComponent<HurtFunction>().hurt = true;
      }

      things = new List<GameObject>();
      
   }

   bool RayCheck(Transform pos, string name)
   {
      Vector3 dir = pos.position - GMController.gm.temp.transform.position;
      RaycastHit2D hit = Physics2D.Raycast(GMController.gm.temp.transform.position, dir, 100, ~(1 << 7 | 1<< 2 | 1 << 8));
      if (hit.collider != null)
      {
         
         if (hit.collider.gameObject.name == name) return true;
         
      }
       return false;
   }
}
