using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private string[] lines;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private float typingSpeed;
    [SerializeField] private int[] playerIndex, otherIndex;
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private SpriteRenderer[] icons;
    private DialogueStorage ds;
    private int index,charIndex,iconIndex;
    
    void Start()
    {
       
        ds = GetComponent<DialogueStorage>();
        index = 0;
        StartDialouge();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if(dialogueText.text == lines[index]) NextLine();
            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
        }
    }

    void StartDialouge()
    {
        StartCoroutine(DisplayLine());
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(DisplayLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
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
