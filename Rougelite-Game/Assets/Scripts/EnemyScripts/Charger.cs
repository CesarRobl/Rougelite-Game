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
    [SerializeField] private GameObject chargeField;
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

    void PlayScream()
    {
        TempSound.soundtemp.ChangePitch(SoundControl.Soundcntrl.EnemyAS, new []{.75f, .65f}, 1);
        SoundControl.Soundcntrl.EnemyAS.PlayOneShot(TempSound.soundtemp.monsterScream);
        SoundControl.Soundcntrl.EnemyAS.pitch = 1;
    }

    IEnumerator Charge()
    {
        anim = true;
        attackSign.SetActive(true);
        _sprite.sprite = prepareCharge[movementDir.spriteNum];
        PlayScream();
        yield return new WaitForSeconds(attackDelay);
        chargeField.SetActive(true);
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
        chargeField.SetActive(false);
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

    public new void OnTriggerEnter2D(Collider2D col)
    {
        ObstacleScript os = col.GetComponent<ObstacleScript>();
        if (os != null)
        {
            StartCoroutine(os.HitParticle());
            os.HP = 0;
        }
    }
}
