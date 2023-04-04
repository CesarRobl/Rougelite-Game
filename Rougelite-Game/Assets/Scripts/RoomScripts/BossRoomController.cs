using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomController : MonoBehaviour
{

    [SerializeField] private SpriteRenderer[] doors;
    [SerializeField] private Sprite[] closed;
    [SerializeField] private GameObject wall;
    [SerializeField]private Sprite[] opened;
    [SerializeField] private GameObject boss;
    [SerializeField] private DialogueList bossTalk;
    [HideInInspector] private TempPortalScript portal;
    private SpawnController spawner;
    private bool stop,done,open;
    private int num;
    void Awake()
    {
        spawner = GetComponentInChildren<SpawnController>();
        portal = GetComponentInChildren<TempPortalScript>();
        for (int i = 0; i < doors.Length; i++)
        {
              opened[i] = doors[i].sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(done) CheckBoss();
        if (open & !portal.done) StartCoroutine(portal.PortalGrow());
    }

    void CheckBoss()
    {
        if (boss == null & !open)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].sprite = opened[i];
            }
            wall.SetActive(false);
            open = true;
        }
    }
    void Setup()
    {
        
        boss.SetActive(true);
        wall.SetActive(true);
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].sprite = closed[i];
        }
        
        done = true;
    }

    IEnumerator TalkFirst()
    {
        GMController.gm.ShowDialogue(bossTalk);
        if (!GMController.gm.dialogue) num = 1;
       yield return new  WaitForSeconds(.1f);
       stop = true;
        Setup();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.CompareTag("TestPlayer") & !stop)
        {
            Debug.Log("Spawn the boss");
            StartCoroutine(TalkFirst());
        }
    }
}
