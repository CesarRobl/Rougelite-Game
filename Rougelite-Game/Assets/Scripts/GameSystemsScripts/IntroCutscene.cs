using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroCutscene : MonoBehaviour
{
    public TextMeshProUGUI[] text;
    [SerializeField] private int scene;
    [SerializeField] private GameObject scenes;
    void Start()
    {
        StartCoroutine(Cutscene());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) scene++;
       
       scenes.transform.position -= new Vector3(3f * Time.deltaTime, 0, 0);
    }

    IEnumerator Cutscene()
    {
        yield return new WaitForSeconds(6);
        while(text[0].color.a > 0)text[0].color -= new Color(0, 0, 0, .05f * Time.deltaTime);
        yield return new WaitForSeconds(1f);
        Debug.Log("Stop");
        while(text[1].color.a < 1)text[1].color+= new Color(0, 0, 0, .05f * Time.deltaTime);
        yield return new WaitForSeconds(4f);
        while(text[1].color.a > 0)text[1].color -= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(.5f);
        while(text[2].color.a < 1)text[2].color+= new Color(0, 0, 0, .05f * Time.deltaTime);
        yield return new WaitForSeconds(4f);
        while(text[2].color.a > 0)text[2].color -= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(.5f);
        while(text[3].color.a < 1)text[3].color+= new Color(0, 0, 0, .05f * Time.deltaTime);
        yield return new WaitForSeconds(4f);
        while(text[3].color.a > 0)text[3].color -= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(.5f);
        while(text[4].color.a < 1)text[4].color+= new Color(0, 0, 0, .05f * Time.deltaTime);
        yield return new WaitForSeconds(4f);
        while(text[4].color.a > 0)text[4].color -= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(.5f);
        while(text[5].color.a < 1)text[5].color+= new Color(0, 0, 0, .05f * Time.deltaTime);
        yield return new WaitForSeconds(4f);
        while(text[5].color.a > 0)text[5].color -= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(.5f);
        while(text[6].color.a < 1)text[6].color+= new Color(0, 0, 0, .05f * Time.deltaTime);
        yield return new WaitForSeconds(4f);
        while(text[6].color.a > 0)text[6].color -= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(.5f);
        while(text[7].color.a < 1)text[7].color+= new Color(0, 0, 0, .05f * Time.deltaTime);
        yield return new WaitForSeconds(4f);
        while(text[7].color.a > 0)text[7].color -= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(.5f);
        while(text[8].color.a < 1)text[8].color+= new Color(0, 0, 0, .05f * Time.deltaTime);
        yield return new WaitForSeconds(4f);
        while(text[8].color.a > 0)text[8].color -= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(.5f);
        while(text[9].color.a < 1)text[9].color+= new Color(0, 0, 0, .05f * Time.deltaTime);
        yield return new WaitForSeconds(4f);
        while(text[9].color.a > 0)text[9].color -= new Color(0, 0, 0, .05f);
        yield return new WaitForSeconds(.5f);
       
    }
}
