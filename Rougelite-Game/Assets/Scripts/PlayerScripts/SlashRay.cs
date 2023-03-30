using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashRay : MonoBehaviour
{
   [SerializeField]private List<GameObject> things;
   private bool stop;

   private void OnTriggerEnter2D(Collider2D col)
   {
     
      HurtFunction ow = col.gameObject.GetComponent<HurtFunction>();
      if (ow != null & !stop)
      {
         things.Add(col.gameObject);
         HitCheck();
         stop = true;
      }
   }

   void HitCheck()
   {
      foreach (var thing in things)
      {
         Debug.Log("I hit " + thing);
         thing.GetComponent<HurtFunction>().hurt = true;
         things.Remove(thing);
      }

      stop = false;
   }
}
