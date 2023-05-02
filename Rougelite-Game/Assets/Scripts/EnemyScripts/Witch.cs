using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Witch :TestAI
{
    [HideInInspector] public Slider health;
    [HideInInspector] public ParticleSystem[] teleport, explode;
    [SerializeField] private ParticleSystem shield, tele;
    [HideInInspector] public GameObject[] minions;
    public Sprite[] wendyBrooms;
    [SerializeField] public float shootDelay;
    private float pastDelay;
    private int rushLimit,hellLimit;
    private bool stop,rushing, _startPhase,_phaseDone;
    private Vector3 _firstPos;
    private SpriteRenderer sprite;
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        cooldownMax = Random.Range(2, 3);
        _firstPos = transform.position;
        Setup();
    }

    
    void Update()
    {
        if (!GMController.gm.playerDead) AI();
        else gameObject.SetActive(false);
    }

    void AI()
    {
        AttackDir(GMController.gm.oc.witch);

        if (!GMController.gm.dialogue)
        {
            if (!stop) SpawnBar();
            MatchSliderValue();
            if (cooldownInt > cooldownMax)
            {
                sprite.sprite = wendyBrooms[0];
                ResetValues();
                StartCoroutine(DownTime(GetComponent<SpriteRenderer>(), Color.white));
            }

            if (!cooldown)
            {
                if (HP <= 10 & !_phaseDone)
                {
                    StopAllCoroutines();
                    StartCoroutine(SecondPhase());
                    _phaseDone = true;
                }

                SeekPlayer();
                AttackRange(~(1 << 0 | 1 << 2 | 1 << 6));
                
                if (!_startPhase)
                {

                    if (found & !attack)
                    {
                        sprite.sprite = wendyBrooms[5];
                        MoveToPlayer();
                    }
                    if (attack & !attacking) Attack();
                }
            }


           
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
        ai.enabled = true;
        attacking = true;
        int ran = Random.Range(1, 4);
        cooldownInt++;
        if(cooldownInt <= cooldownMax) SwitchAttacks(ran);

    }

    void ResetValues()
    {
        ai.enabled = false;
        attackSign.SetActive(false);
        cooldownMax = Random.Range(2, 3);
        cooldownInt = 0;
        cooldown = true;
        RB.velocity = Vector3.zero;
        attacking = false;
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
        attackSign.SetActive(true);
        sprite.sprite = wendyBrooms[1];
        yield return new WaitForSeconds(attackDelay);
        sprite.sprite = wendyBrooms[2];
        attackSign.SetActive(false);
        ai.enabled = false;
        yield return new WaitForSeconds(.1f);
        RB.velocity = (dir.normalized * (GMController.gm.maxforce));
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
        sprite.sprite = wendyBrooms[3];
        StartCoroutine(ConePellets());
        yield return new WaitForSeconds(.3f);
        StartCoroutine(ConePellets());
        yield return new WaitForSeconds(.3f);
        StartCoroutine(ConePellets());
        yield return new WaitForSeconds(1.5f);
        attacking = false;
    }
    
    IEnumerator ConePellets()
    {
        attackSign.SetActive(true);
        yield return new WaitForSeconds(attackDelay);
        attackSign.SetActive(false);
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
        attackSign.SetActive(true);
        sprite.sprite = wendyBrooms[3];
        yield return new WaitForSeconds(attackDelay - .5f);
        attackSign.SetActive(false);
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

    IEnumerator SecondPhase()
    {
        tele.Play();
        yield return new WaitForSeconds(.1f);
        _startPhase = true;
        shield.gameObject.SetActive(true);
        shield.Play();
        transform.position = _firstPos;
        RB.constraints = RigidbodyConstraints2D.FreezeAll;
       ResetValues();
       cooldown = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        for (int i = 0; i < teleport.Length; i++) teleport[i].Play();
        

        yield return new WaitForSeconds(3.5f);
        for (int i = 0; i < minions.Length; i++)
        {
            teleport[i].Stop();
            explode[i].Play();
            minions[i].SetActive(true);
        }
        RB.constraints = RigidbodyConstraints2D.FreezeRotation;
        shield.gameObject.SetActive(false);
        ai.enabled = true;
        _startPhase = false;

    }

    public new void OnCollisionEnter2D(Collision2D col)
    {
        if(rushing & !col.gameObject.CompareTag("Enemy"))StartCoroutine(RepeatRush());
    }
}
