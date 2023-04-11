using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Witch :TestAI
{
    [HideInInspector] public Slider health;
    [SerializeField] public float shootDelay;
    private float pastDelay;
    private int rushLimit,hellLimit;
    private bool stop,rushing;
    void Awake()
    {
        
        Setup();
    }

    
    void Update()
    {
        AttackDir(GMController.gm.oc.witch);   
      
        if (!GMController.gm.dialogue)
        {
            if (!stop) SpawnBar();
            MatchSliderValue();
            SeekPlayer();
            AttackRange();
            if(found & !attack) MoveToPlayer();
            if (attack & !attacking) Attack();
            Enemyhit();
            
               
            if (rushLimit > 2)
                {
                    StopAllCoroutines();
                    rushLimit = 0;
                    ai.enabled = true;
                    attacking = false;
                    rushing = false;
                }
            
        }
    }

    public override void Attack()
    {
        attacking = true;
        int ran = Random.Range(1, 4);
        SwitchAttacks(ran);
        
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

    void SwitchAttacks(int num)
    {
        switch (num)
        {
            case 1:
            {
                StartCoroutine(ConeAttack());
                break;
            }
            case 2:
            {
                rushing = true;
                StartCoroutine(Rush());
                break;
            }
            case 3:
            {
                Quaternion rot = quaternion.identity;
                StartCoroutine(BulletHell(rot));
                break;
            }
        }
    }

    IEnumerator Rush()
    {
        ai.enabled = false;
        yield return new WaitForSeconds(.3f);
        RB.velocity = (dir.normalized * (GMController.gm.maxforce - 10));
        yield return new WaitForSeconds(.1f);
        // StartCoroutine(RepeatRush());
    }

    IEnumerator RepeatRush()
    {
        RB.velocity = Vector2.zero;
        yield return new WaitForSeconds(.5f);
        rushLimit++;
        StartCoroutine(Rush());
    }

    IEnumerator ConeAttack()
    {
        attacking = true;
        StartCoroutine(ConePellets());
        yield return new WaitForSeconds(1f);
        StartCoroutine(ConePellets());
        yield return new WaitForSeconds(1f);
        StartCoroutine(ConePellets());
        yield return new WaitForSeconds(1.5f);
        attacking = false;
    }
    
    IEnumerator ConePellets()
    {
        
        Quaternion rot = quaternion.identity;
        rot.eulerAngles = movementDir.gameObject.transform.eulerAngles;
        rot.eulerAngles -=new Vector3(0,0,45);
        for (int i = 0; i < 6; i++)
        {
            Instantiate(GMController.gm.oc.enemypellet, transform.position, Quaternion.Euler(rot.eulerAngles));
            rot.eulerAngles +=new Vector3(0,0,22.5f);
        }
        yield return new WaitForSeconds(.1f);
       
    }

    
    IEnumerator BulletHell(Quaternion rot)
    {
       
        rot.eulerAngles = movementDir.gameObject.transform.eulerAngles;
        rot.eulerAngles -=new Vector3(0,0,45);
        for (int i = 0; i < 17; i++)
        {
            Instantiate(GMController.gm.oc.enemypellet, transform.position, Quaternion.Euler(rot.eulerAngles));
            rot.eulerAngles +=new Vector3(0,0,22.5f);
        }
        yield return new WaitForSeconds(.1f);
        StartCoroutine(RepeatHell());
    }

    IEnumerator RepeatHell()
    {
        Quaternion rot = quaternion.identity;
        yield return new WaitForSeconds(1f);
        StartCoroutine(BulletHell(rot));
        hellLimit++;
        yield return new WaitUntil(() => hellLimit >= 6);
        StartCoroutine(StopHell());
    }

    IEnumerator StopHell()
    {
        yield return new WaitForSeconds(.1f);
        hellLimit = 0;
        attacking = false;
        StopAllCoroutines();
    }

    public new void OnCollisionEnter2D(Collision2D col)
    {
        if(rushing)StartCoroutine(RepeatRush());
    }
}
