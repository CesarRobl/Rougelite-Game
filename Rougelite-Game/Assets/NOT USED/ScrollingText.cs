using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollingText : MonoBehaviour
{
   [Header("Text Settings")]
   [SerializeField] [TextArea] private string[] iteminfo;
   [SerializeField] private float textSpeed = 0.01f;
   
   

   [Header("UI Elements")]
   [SerializeField] private TextMeshProUGUI itemInfoText;
   private int currentDisplayingText = 0;

   public void ActivateText()
   {
      StartCoroutine(AnimateText());

   }

   IEnumerator AnimateText()
   {
      for (int i = 0; i < iteminfo[currentDisplayingText].Length +1; i++)
      {
         iteminfo[currentDisplayingText].Substring(0, i);
         yield return new WaitForSeconds(textSpeed);
      }
   }

}
