using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotShotEnemy : TestAI
{
    private Animator slash;
    private Vector2 currentpos,pos;
    [SerializeField] private Vector2 lastpos;
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
            SeekPlayer();
            AttackRange();
            Enemyhit();
            if (found & !stun & !anim)
            {
                
                MoveToPlayer();
                SpriteDir(GMController.gm.oc.phantom);
            }
           
            if (attacking)
            {
                dist = Vector2.Distance(currentpos, pos);
                dist2 = Vector2.Distance(transform.position, lastpos);
                StartCoroutine(Rush());
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
        collide.isTrigger = true;
        GetComponent<SpriteRenderer>().color = Color.gray;
        transform.position = Vector3.MoveTowards(transform.position, pos, 6 * Time.deltaTime);
        yield return new WaitUntil(() => dist < .3f);
        pos = transform.position;
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
        
        transform.position = Vector3.MoveTowards(transform.position, lastpos, 10 * Time.deltaTime);
       
        yield return new WaitUntil(() => dist2 < .1f);
        yield return new WaitForSeconds(.1f);
        ai.enabled = true;
        attacking = false;
        yield return new WaitForSeconds(1.5f);
        anim = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, dir.normalized * attackrange);
    }
}
