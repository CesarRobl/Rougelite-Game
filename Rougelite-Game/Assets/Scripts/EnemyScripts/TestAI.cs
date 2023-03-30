using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class TestAI : MonoBehaviour
{
    public int HP;
    public bool found;
    public float drange;
    public float speed = 2;
    public float attackrange;
    public bool attack;
    public float forceResistance;
    [HideInInspector]public Vector3 pastpos;
    public Vector2 dir;
    [HideInInspector] public Rigidbody2D RB;
    [HideInInspector] public bool Stop,stun,attacking,anim;
    [HideInInspector] public ParticleSystem ps;
    [HideInInspector] public AIPath ai;
    [HideInInspector] public HurtFunction ow;
    private TempPlayer pc;
    
    private void Awake()
    {
        Setup();
    }

    void Update()
    {
        if (found & !stun)
        {
          MoveToPlayer();
        }
        SeekPlayer();
        Enemyhit();
    }

    public void Setup()
    {
        RB = GetComponent<Rigidbody2D>();
        ps = GetComponentInChildren<ParticleSystem>();
        ai = GetComponent<AIPath>();
        ow = GetComponent<HurtFunction>();
        pastpos= transform.position;
    }

    virtual public void Attack()
    {
        
    }

    public void MoveToPlayer()
    {
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
            Debug.Log(hit.collider.gameObject.name);
            TempPlayer pc = hit.collider.GetComponent<TempPlayer>();
            if (pc != null)
            {
                found = true;
            }
            
        }
    }

      public void AttackRange()
      {
          
          RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, attackrange,~(1<<0 | 1<< 2));
          if (hit.collider != null)
          {

              pc = hit.collider.GetComponent<TempPlayer>();
              if (pc != null)
              {
                  attack = true;
              }
              else attack = false;

          }
          else
          {
              attack = false;
          }
         

      }

    
    private void OnCollisionEnter2D(Collision2D col)
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
        if (Stop == false)
        {
            Stop = true;
            StartCoroutine(EnemyHurt(ps));
            if(!attacking)Knockback(GMController.gm.maxforce);
                
        }
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
        sr.color = og;
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
}
