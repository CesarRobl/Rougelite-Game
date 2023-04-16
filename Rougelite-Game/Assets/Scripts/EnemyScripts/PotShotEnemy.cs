using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotShotEnemy : TestAI
{
    private Animator slash;
    private Vector3 currentpos,pos;
    [SerializeField] private Vector3 lastpos;
    private float dist,dist2;
    [SerializeField] private GameObject[] slashes;
    private BoxCollider2D collide;
    
    void Awake()
    {
        Setup();
        collide = GetComponent<BoxCollider2D>();
        slash = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentpos = transform.position;
            AttackDir(GMController.gm.oc.phantom);
            SeekPlayer();
            AttackRange(~(1<<0 | 1<< 2 | 1 << 10 | 1 << 8));
            Enemyhit();
            if (found & !stun & !anim)
            {
                MoveToPlayer();
            }
           
            if (attacking)
            {
                dist = Vector2.Distance(currentpos, pos);
                dist2 = Vector2.Distance(transform.position, lastpos);
            }
            
            if (attack & !anim) Attack();
            
        

}

    public override void Attack()
    {
        anim = true;
        attacking = true;
        pos = GMController.gm.temp.transform.position;
        lastpos = transform.position;
        StartCoroutine(Rush());
    }

    IEnumerator Rush()
    {
        ai.destination = transform.position;
        ai.enabled = false;
        pos = GMController.gm.player.position;
        collide.isTrigger = true;
        
        Vector2 posDir = pos - transform.position;
        RB.velocity = posDir.normalized * 9.5f;
        GetComponent<SpriteRenderer>().color = new Color(256,256,256, .5f);
        
        yield return new WaitUntil(() => dist < .4f);
        RB.velocity = Vector2.zero;
        StartCoroutine(SlashAttack());
    }

    IEnumerator SlashAttack()
    {
        collide.isTrigger = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(.1f);
        slash.SetTrigger("Slash");
        yield return new WaitForSeconds(1f);
        slash.SetTrigger("Slash");
        yield return new WaitForSeconds(1f);
        StartCoroutine(GoBack());
    }

    IEnumerator GoBack()
    { 
        collide.isTrigger = true;
        GetComponent<SpriteRenderer>().color = Color.gray;
        Vector2 posDir = lastpos - transform.position;
        RB.velocity = posDir.normalized * 8;
        yield return new WaitUntil(() => dist2 < .4f);
        RB.velocity = Vector2.zero;
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(.1f);
        ai.enabled = true;
        attacking = false;
        yield return new WaitForSeconds(1f);
        anim = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, dir.normalized * attackrange);
    }
}
