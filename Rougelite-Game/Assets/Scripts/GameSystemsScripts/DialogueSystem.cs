using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
   
    [SerializeField] private string[] lines;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private float typingSpeed;
    [SerializeField] private int[] maxLines;
    [SerializeField] private Sprite[] icons;
    [SerializeField] private Image icon;
    [SerializeField] private DialogueList talk;
    private DialogueStorage ds;
    private int index,iconIndex,lineIndex,talkIndex;
    
    void Awake()
    {
      
        StartDialogue(talk);
    }

   
    void Update()
    {
       
       
        if (Input.anyKeyDown)
        {
            if (dialogueText.text == lines[index])
            {
                if (talkIndex == talk.maxLines[lineIndex])
                {
                    ChangeIcon();
                }
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
        }
    }

   public void StartDialogue(DialogueList chat)
    {
       
        GMController.gm.dialogue = true;
        talk = chat;
        index = 0;
        iconIndex = 0;
        lineIndex = 0;
        talkIndex = 0;
        ChangeDialogue(chat);
        StartCoroutine(DisplayLine());
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            talkIndex++;
            StartCoroutine(DisplayLine());
        }
         if(index >= lines.Length - 1)
         {
             GMController.gm.dialogue = false;
            index = 0;
            gameObject.SetActive(false);
        }
    }

    void ChangeIcon()
    {
        talkIndex = 0;
        lineIndex++;
        iconIndex++;
        icon.sprite = talk.icons[iconIndex];
    }

    void ChangeDialogue(DialogueList talks)
    {
        lines = talks.lines;
        maxLines = talks.maxLines;
        icon.sprite = talks.icons[0];
        
    }

    IEnumerator DisplayLine()
    {
        dialogueText.text = "";
        foreach (char letter in lines[index])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
