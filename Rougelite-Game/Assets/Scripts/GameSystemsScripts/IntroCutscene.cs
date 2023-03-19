using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroCutscene : MonoBehaviour
{
    public TextMeshProUGUI[] text;
    [SerializeField] private int scene;
    [SerializeField] private GameObject[] scenes;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) scene++;
       StartCoroutine(Cutscene());
    }

    IEnumerator Cutscene()
    {
        yield return new WaitForSeconds(1);
        text[0].color -= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(.25f);
        text[1].color+= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(1);
        text[1].color -= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(.25f);
        /*text[2].color+= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(1);
        text[2].color -= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(.25f);
        text[3].color+= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(1);
        text[3].color -= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(.25f);
        text[4].color += new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(1);
        text[4].color -= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(.25f);
        text[5].color+= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(1);
        text[5].color -= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(.25f);
        text[6].color+= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(1);
        text[6].color -= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(.25f);
        text[7].color+= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(1);
        text[7].color -= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(.25f);
        text[8].color+= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(1);
        text[8].color -= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(.25f);
        text[9].color+= new Color(0, 0, 0, .05f);*/
    }
}
