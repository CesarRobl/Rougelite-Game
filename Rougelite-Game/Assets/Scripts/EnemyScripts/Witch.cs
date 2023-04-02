using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Witch :TestAI
{
    [HideInInspector] public Slider health;
    private bool stop;
    void Awake()
    {
        Setup();
    }

    
    void Update()
    {
        if(!stop) SpawnBar();
        MatchSliderValue();
        SeekPlayer();
        AttackRange();
        if(attack) Attack();
       Enemyhit();
    }

    public override void Attack()
    {
        Debug.Log("I am attacking");
    }

    void SpawnBar()
    {
        GMController.gm.ui.ShowBossBar(true);
        health = GMController.gm.ui.BossBar.GetComponent<Slider>();
        health.maxValue = HP;
        health.value = HP;
        stop = true;
    }

    void MatchSliderValue()
    {
        health.value = HP;
    }
}
