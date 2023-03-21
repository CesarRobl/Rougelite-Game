using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotShotEnemy : TestAI
{
    private bool attacking;
    private Vector2 currentpos,pos;
    [SerializeField] private Vector2 lastpos;
    private float dist,dist2;
    void Awake()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
        currentpos = transform.position;
        SeekPlayer();
        AttackRange();
        if (found & !stun & !attack & !attacking)
        {
            MoveToPlayer();
        }

        if (attacking)
        {
            dist = Vector2.Distance(currentpos, pos);
            dist2 = Vector2.Distance(transform.position, lastpos);
        }
        
        if(attack & !attacking)Attack();
        
        if(HP <= 0) GMController.gm.Die(gameObject, GetComponent<LootSystem>());
    }

    public override void Attack()
    {
        attacking = true;
        StartCoroutine(Rush());
    }

    IEnumerator Rush()
    {
        
         pos = GMController.gm.temp.transform.position;
        lastpos = transform.position;
        ai.destination = pos;
        ai.speed = 25;
       

        yield return new WaitUntil(() => dist < .1f);
        StartCoroutine(GoBack());
    }

    IEnumerator GoBack()
    {
        
        ai.destination = lastpos;
       
        yield return new WaitUntil(() => dist2 < .1f);
        yield return new WaitForSeconds(.5f);
        ai.speed = speed;
        attacking = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, dir.normalized * attackrange);
    }
}
