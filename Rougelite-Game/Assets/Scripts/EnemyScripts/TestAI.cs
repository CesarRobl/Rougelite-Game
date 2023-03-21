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
    
    [HideInInspector]public Vector3 pastpos;
    public Vector2 dir;
    [SerializeField] public Rigidbody2D RB;
    [SerializeField] public bool Stop,stun,attacking;
    [SerializeField] public ParticleSystem ps;
    [SerializeField] public AIPath ai;
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
        if(HP <= 0) GMController.gm.Die(gameObject, GetComponent<LootSystem>());
        
        
    }

    public void Setup()
    {
        RB = GetComponent<Rigidbody2D>();
        ps = GetComponentInChildren<ParticleSystem>();
        ai = GetComponent<AIPath>();
        pastpos= transform.position;
    }

    virtual public void Attack()
    {
        
    }

    public void MoveToPlayer()
    {
        ai.maxSpeed = speed;
        ai.destination = GMController.gm.temp.transform.position;
    }

      public void SeekPlayer()
    {
         dir = GMController.gm.player.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, drange, ~LayerMask.NameToLayer("Pellet"));
        if (hit.collider != null)
        {
            
            TempPlayer pc = hit.collider.GetComponent<TempPlayer>();
            if (pc != null)
            {
                found = true;
            }
            
        }
    }

      public void AttackRange()
      {
          
          RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, attackrange,~LayerMask.NameToLayer("Pellet"));
          if (hit.collider != null)
          {

              pc = hit.collider.GetComponent<TempPlayer>();
              if (pc != null)
              {
//                  Debug.Log(hit.collider.gameObject.name);
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
        if (other.gameObject.CompareTag("Sword"))
        {
            HP--;
            if (Stop == false)
            {
                Stop = true;
                StartCoroutine(EnemyHurt(ps));
                if(!attacking)Knockback(GMController.gm.maxforce);
            }
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
        ai.destination = Vector3.zero;
        
        RB.AddForce(-dir.normalized * GMController.gm.maxforce, ForceMode2D.Impulse);
        stun = true;
        StartCoroutine(Reset());
        Stop = false;
        
    }

     IEnumerator Reset()
     {
         ai.maxSpeed = speed;
        yield return new WaitForSeconds(GMController.gm.forcedelay);
        ai.destination = GMController.gm.temp.transform.position;
        RB.velocity = Vector2.zero;
        stun = false;
    }
}
