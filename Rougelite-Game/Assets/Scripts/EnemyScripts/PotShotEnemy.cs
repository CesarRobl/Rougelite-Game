using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotShotEnemy : TestAI
{
    private Animator slash;
    private Vector3 currentpos,pos;
    [SerializeField] private Vector3 lastpos;
    private float dist,dist2;
    [SerializeField] private GameObject slashes;
    private Vector3[] _rot =  {new Vector3(0, 0, 90), new Vector3(0,0,270), new Vector3(0,0,180), new Vector3(0,0,0)}; 
    private BoxCollider2D collide;
    [SerializeField]private Sprite[] attackSprites;
    
    void Awake()
    {
        cooldown = true;
        StartCoroutine(Wait());
        Setup();
        collide = GetComponent<BoxCollider2D>();
        slash = GetComponentInChildren<Animator>();

        enemyAni.slashes = slashes;
        enemyAni._rot = _rot;
        StartCoroutine(enemyAni.Idle());
    }

    // Update is called once per frame
    void Update()
    {
        if (!GMController.gm.playerDead) AI();

    }

    void AI()
    {
        
        currentpos = transform.position;
      
        if(anim)AttackDir(GMController.gm.oc.phantom);
        else SpriteDir(GMController.gm.oc.phantom);
        
        dist = Vector2.Distance(currentpos, pos);
        dist2 = Vector2.Distance(transform.position, lastpos);
       
        SeekPlayer();
        if(!attacking)AttackRange(~(1<<0 | 1<< 2 | 1 << 10 | 1 << 8));
        Enemyhit();

        if (!cooldown)
        {
            if (found & !stun & !anim)
            {
                MoveToPlayer();
                enemyAni.idleStop = true;
                if(!enemyAni.walkPlaying)StartCoroutine(enemyAni.WalkAni());
            }

            if (attack & !anim) Attack();
        }

       
    }
    public override void Attack()
    {
        enemyAni.idleStop = true;
            anim = true;
            attacking = true;
            lastpos = transform.position;
            pos = GMController.gm.player.position;
            enemyAni.walkStop = true;
            enemyAni.walkPlaying = false;
            if (pos != Vector3.zero)
            {
                // Debug.Log(pos);
                StartCoroutine(Rush());
            }
            else
            {
                enemyAni.walkStop = false;
                attack = false;
                attacking = false;
                anim = false;
            }
    }

    private void ChangeAttackDir()
    {
        
    }
    IEnumerator Rush()
    {
        // ai.destination = transform.position;
        ai.enabled = false;
        collide.isTrigger = true;
        enemyAni.walkStop = true;
        enemyAni.walkPlaying = false;
        enemyAni.attackStop = false;
       
       
        attackSign.SetActive(true);
        SoundControl.Soundcntrl.EnemyAS.PlayOneShot(TempSound.soundtemp.phantomNoise);
        yield return new WaitForSeconds(attackDelay + .5f);
        Vector2 posDir = pos - transform.position;
        attackSign.SetActive(false);
        RB.velocity = posDir.normalized * 15.5f;
        GetComponent<SpriteRenderer>().color = new Color(256,256,256, .5f);
        
        yield return new WaitUntil(() => dist < .4f);
        RB.velocity = Vector2.zero;
        StartCoroutine(SlashAttack());
    }

    IEnumerator SlashAttack()
    {
       
        collide.isTrigger = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        attackSign.SetActive(false);
        
        yield return new WaitForSeconds(attackDelay);
        attackSign.SetActive(true);
        StartCoroutine(enemyAni.PhantomSlash(slash));
        // GetComponent<SpriteRenderer>().sprite = attackSprites[0];
        yield return new WaitForSeconds(attackDelay );
        attackSign.SetActive(false);
      
        // GetComponent<SpriteRenderer>().sprite = attackSprites[1];
       
        
       
       
        yield return new WaitForSeconds(attackDelay + .9f);
       
        // GetComponent<SpriteRenderer>().sprite = attackSprites[1];
       
        
        
        yield return new WaitForSeconds(.1f);
        StartCoroutine(DownTime(GetComponent<SpriteRenderer>(), new Color(256, 256, 256, .5f)));
    }

    IEnumerator GoBack()
    { 
        collide.isTrigger = true;
        GetComponent<SpriteRenderer>().sprite = GMController.gm.oc.phantom[movementDir.spriteNum];
        Vector2 posDir = lastpos - transform.position;
        RB.velocity = posDir.normalized * 8;
        yield return new WaitUntil(() => dist2 < .4f);
        RB.velocity = Vector2.zero;
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(1f);
        ai.enabled = true;
        ai.destination = transform.position;
        attack = false;
        attacking = false;
        anim = false;
    }

    public override IEnumerator DownTime(SpriteRenderer spriteColor, Color ogColor)
    {
       enemyAni.StopSlash();
        spriteColor.color = Color.grey;
        yield return new WaitForSeconds(cooldownRate);
        spriteColor.color = ogColor;
        cooldownInt = pastCool;
        cooldown = false;
        StartCoroutine(GoBack());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.3f);
        cooldown = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, dir.normalized * attackrange);
    }
}
