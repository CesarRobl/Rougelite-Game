using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Pathfinding;
using UnityEngine;

public class TestAI : MonoBehaviour
{
    public int HP, cooldownInt,cooldownMax;
    public bool found;
    public float drange,cooldownRate,attackDelay;
    public float speed = 2;
    public float attackrange;
    public bool attack;
    public float forceResistance;
    public GameObject attackSign;
    public PlayerDirFinder movementDir;
    [HideInInspector]public Vector3 pastpos;
    public Vector2 dir;
    [HideInInspector] public int pastCool;
    [HideInInspector] public Rigidbody2D RB;
    [HideInInspector] public bool Stop,stun,attacking,anim,cooldown;
    [HideInInspector] public ParticleSystem ps;
    [HideInInspector] public AIPath ai;
     public HurtFunction ow;
    private TempPlayer pc;
   
    
    private void Awake()
    {
        Setup();
    }

    void Update()
    {
        SpriteDir(GMController.gm.oc.normalEnemy);
        if (found & !stun)
        {
          MoveToPlayer();
        }
        SeekPlayer();
        Enemyhit();
    }

    public void Setup()
    {
        movementDir = GetComponentInChildren<PlayerDirFinder>();
        RB = GetComponent<Rigidbody2D>();
        ps = GetComponentInChildren<ParticleSystem>();
        ai = GetComponent<AIPath>();
        ow = GetComponent<HurtFunction>();
        pastpos= transform.position;
        pastCool = cooldownInt;
    }

    virtual public void Attack()
    {
        
    }

    public void MoveToPlayer()
    {
        if (GMController.gm.playerDead) gameObject.SetActive(false);
        if (!stun)
        { 
            ai.maxSpeed = speed;
            ai.destination = GMController.gm.temp.transform.position;
        }
    }
      
      public void SeekPlayer()
    {
         dir = GMController.gm.player.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, drange, ~(1<<0 | 1<< 2));
        if (hit.collider != null)
        {
          
            TempPlayer pc = hit.collider.GetComponent<TempPlayer>();
            if (pc != null)
            {
                found = true;
            }
            
        }
    }

      public void AttackRange(LayerMask mask)
      {
          
          RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, attackrange,mask);
          if (hit.collider != null)
          {
              pc = hit.collider.GetComponent<TempPlayer>();
              if (pc != null)
              {
                  attack = true;
                  return;
              }
              else attack = false;

          }
          else
          {
              attack = false;
          }
         

      }

      // void DirFinder()
      // {
      //     Vector3 pos = new Vector3(ai.steeringTarget.x, ai.steeringTarget.y,ai.steeringTarget.z);
      //     Vector2 dir = pos - transform.position;
      //     float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
      //     Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
      //     transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f * Time.deltaTime);
      // }
    public void SpriteDir(Sprite[] sprites)
      {
         movementDir.ChangeSprite(movementDir.Dir(ai.steeringTarget), sprites, GetComponent<SpriteRenderer>());
      }

    public void AttackDir(Sprite[] sprites)
    {
        movementDir.ChangeSprite(movementDir.PlayerDir(), sprites, GetComponent<SpriteRenderer>());
    }

    public void ChangeSprite(Sprite[] sprites, SpriteRenderer render)
    {
        movementDir.SpriteStateChange(sprites[movementDir.spriteNum], render);
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        TempPlayer tp = col.gameObject.GetComponent<TempPlayer>();
        if (tp != null & !GMController.gm.playerhurt)
        {
           GMController.gm.temp.Playerhurt();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.gameObject.CompareTag("Sword"))
        // {
        //     HP--;
        //     if (Stop == false)
        //     {
        //         Stop = true;
        //         StartCoroutine(EnemyHurt(ps));
        //         if(!attacking)Knockback(GMController.gm.maxforce);
        //     }
        // }
    }

    public void Enemyhit()
    {
        if (ow.hurt)
        {
            Hurt();
            ow.hurt = false;
        }
        if(HP <= 0) GMController.gm.Die(gameObject, GetComponent<LootSystem>());
       
    }

    void Hurt()
    {
        HP--;
        
             StartCoroutine(EnemyHurt(ps));
            if(!attacking & !cooldown)Knockback(GMController.gm.maxforce);
           
       
    }

    // This animation plays when the enemy gets whacked by a player with a sword
    // If you are going to edit this code then please let me know - Cesar
    public IEnumerator EnemyHurt(ParticleSystem hurt)
    {
        
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color og = sr.color;
        hurt.Play();
        sr.color = Color.red;
        yield return new WaitForSeconds(5f * Time.deltaTime);
        if(!cooldown)sr.color = og;
    }
    public void Knockback(float force)
    {
        ai.destination = transform.position;
        ai.enabled = false;
        
        RB.AddForce(-dir.normalized * (GMController.gm.maxforce - forceResistance), ForceMode2D.Impulse);
        stun = true;
        StartCoroutine(Reset());
        Stop = false;
        
    }

     IEnumerator Reset()
     {
         ai.maxSpeed = speed;
        yield return new WaitForSeconds(GMController.gm.forcedelay);
        ai.enabled = true;
        RB.velocity = Vector2.zero;
        stun = false;
    }
     
    public IEnumerator SpawnAfterImage(float imageRate, SpriteRenderer sprite)
     {
         yield return new WaitForSeconds(imageRate);
         GameObject image = Instantiate(GMController.gm.oc.afterImage, gameObject.transform.position, Quaternion.identity);
         image.transform.localScale = sprite.transform.localScale;
         image.GetComponent<SpriteRenderer>().sprite = sprite.sprite;
        
         if (attacking) StartCoroutine(ResetImage(imageRate,sprite));
     }

     IEnumerator ResetImage(float imageRate, SpriteRenderer sprite)
     {
         yield return new WaitForSeconds(.1f);
         StartCoroutine(SpawnAfterImage(imageRate,sprite));
     }

    
     
     public virtual IEnumerator DownTime(SpriteRenderer spriteColor, Color ogColor)
     {
         attack = false;
         spriteColor.color = Color.grey;
         yield return new WaitForSeconds(cooldownRate);
         spriteColor.color = ogColor;
         cooldownInt = pastCool;
         cooldown = false;
     }
}
