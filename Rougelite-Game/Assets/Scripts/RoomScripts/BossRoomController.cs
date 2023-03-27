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
    private SpawnController spawner;
    private bool stop,done,open;
    void Awake()
    {
        spawner = GetComponentInChildren<SpawnController>();
        for (int i = 0; i < doors.Length; i++)
        {
              opened[i] = doors[i].sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(done) CheckBoss();
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.CompareTag("TestPlayer") & !stop)
        {
            Debug.Log("Spawn the boss");
            Setup();
            stop = true;
        }
    }
}
