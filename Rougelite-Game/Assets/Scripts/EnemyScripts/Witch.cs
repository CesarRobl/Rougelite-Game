using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Witch :TestAI
{
    [HideInInspector] public Slider health;
    void Awake()
    {
        Setup();
    }

    
    void Update()
    {
        if(!Stop) SpawnBar();
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
        Stop = true;
    }

    void MatchSliderValue()
    {
        health.value = HP;
    }
}
