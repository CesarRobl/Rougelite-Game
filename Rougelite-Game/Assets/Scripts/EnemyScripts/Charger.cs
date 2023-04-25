using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Charger : TestAI
{
    private bool buffer,stopai;
    [SerializeField] private float chargePower;
    [SerializeField] public Sprite[] prepareCharge;
    [SerializeField] public Sprite[] charge;
    private int chargeNum;
    private float _dist = 10;
    [SerializeField]private SpriteRenderer _sprite;
    void Awake()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(!GMController.gm.playerDead) AI();

    }

    void AI()
    {
        if (found & !stun & !anim)
        {
            MoveToPlayer();
            SpriteDir(GMController.gm.oc.normalEnemy);
        }
        
        if (attack & !attacking & !anim)
        {
           
            Attack();
        }
        
        AttackRange(~(1<<0 | 1<< 2));
        SeekPlayer();
        Enemyhit();

    }

    public override void Attack()
    {
        ai.destination = transform.position;
        ai.enabled = false;
        attacking = true;
        StartCoroutine(Charge());
        
    }

    IEnumerator Charge()
    {
        anim = true;
        attackSign.SetActive(true);
        _sprite.sprite = prepareCharge[movementDir.spriteNum];
        yield return new WaitForSeconds(attackDelay);
        Vector3 posDir = GMController.gm.temp.transform.position - transform.position;
        _sprite.sprite = charge[movementDir.spriteNum];
        attackSign.SetActive(false);
        RB.velocity = posDir.normalized * chargePower;
        StartCoroutine(SpawnAfterImage(.1f, _sprite));
        yield return new WaitForSeconds(1f);
        StartCoroutine(DownTime(_sprite, Color.white));
    }

    public override IEnumerator DownTime(SpriteRenderer spriteColor, Color ogColor)
    {
        spriteColor.color = Color.gray;
        RB.velocity = Vector3.zero;
        attacking = false;
        yield return new WaitForSeconds(cooldownRate);
       
        ai.enabled = true;
        spriteColor.color = ogColor;
        anim = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, dir.normalized * attackrange);
    }
    
    public new void OnCollisionEnter2D(Collision2D col)
    {
        if (attacking & !col.gameObject.CompareTag("Enemy"))
        { 
            StopAllCoroutines();
            Debug.Log("I hit " + col.gameObject.name);
            if (col.gameObject.CompareTag("TestPlayer"))
            {
                GMController.gm.temp.Playerhurt();
            }
            RB.velocity = Vector3.zero;
            StartCoroutine(DownTime(_sprite, Color.white));
        }
    }
}
