using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Charger : TestAI
{
    private bool buffer,stopai,_charging;
    [SerializeField] private float chargePower;
    [SerializeField] public Sprite[] prepareCharge;
    [SerializeField] public Sprite[] charge;
    private int chargeNum;
    private float _dist = 10;
    [SerializeField]private SpriteRenderer _sprite;
    [SerializeField] private GameObject chargeField;
    void Awake()
    {
        StartCoroutine(enemyAni.Idle());
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
            SpriteDir(GMController.gm.oc.charger);
            enemyAni.idleStop = true;
            if (!enemyAni.walkPlaying) StartCoroutine(enemyAni.WalkAni());

        }
        
        if (attack & !attacking & !anim)
        {
          
            Attack();
        }
        
        if(anim || attacking) movementDir.GetSpriteNum();
        
        AttackRange(~(1<<0 | 1<< 2));
        SeekPlayer();
        Enemyhit();

    }

    public override void Attack()
    {
        ai.destination = transform.position;
        ai.enabled = false;
        attacking = true;
        enemyAni.walkStop = true;
        enemyAni.walkPlaying = false;
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
        PlayScream();
        _sprite.sprite = charge[movementDir.spriteNum];
        enemyAni.attackStop = false;
        yield return new WaitForSeconds(attackDelay);
        enemyAni.SwitchAttack();
        StartCoroutine(enemyAni.AttackAni());
        _charging = true;
        chargeField.SetActive(true);
        Vector3 posDir = GMController.gm.temp.transform.position - transform.position;
        
        attackSign.SetActive(false);
        RB.velocity = posDir.normalized * chargePower;
        StartCoroutine(SpawnAfterImage(.1f, _sprite));
        yield return new WaitForSeconds(1f);
        StartCoroutine(DownTime(_sprite, Color.white));
    }

    public override IEnumerator DownTime(SpriteRenderer spriteColor, Color ogColor)
    {
        enemyAni.StopAttack();
        _sprite.sprite = GMController.gm.oc.charger[movementDir.spriteNum];
        _charging = false;
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
        if (_charging & !col.gameObject.CompareTag("Enemy"))
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
