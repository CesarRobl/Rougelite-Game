using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCutscene : MonoBehaviour
{
    [SerializeField] private int scene;
    [SerializeField] private GameObject[] scenes;
    void Start()
    {
        StartCoroutine(Cutscene());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) scene++;
    }

    IEnumerator Cutscene()
    {
        
        yield return new WaitUntil(() => scene == 1);
        scenes[0].SetActive(false);
        scenes[1].SetActive(true);
        
    }
}
