using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretScript : TestAI
{
    [SerializeField] private float shootDelay;
    [SerializeField] private GameObject playerDir;
    private Color _turretColor;
    private bool _delay;

    void Awake()
    {
        _turretColor = GetComponent<SpriteRenderer>().color;
        Setup();
        StartCoroutine(enemyAni.Idle());
    }

    // Update is called once per frame
    void Update()
    {
        if(!GMController.gm.playerDead)AI();
    }

    void AI()
    {
        playerDir.GetComponent<PlayerDirFinder>().PlayerDir();
        SeekPlayer();
        if (!attack & cooldownInt > 0)
        {
            
            AttackRange(~(1<<0 | 1<< 2));
        }
       
        if (attack & cooldownInt > 0)
        {
            
            AttackDir(GMController.gm.oc.shooter);
            if(!attacking)StartCoroutine(ShootPellets());
              
            
        }

        if (cooldownInt <= 0)
        {
            enemyAni.StopAttack();
            StartCoroutine(DownTime(GetComponent<SpriteRenderer>(), _turretColor));
        }
        
        Enemyhit();
    }
    

    public override void Attack()
    {
        SoundControl.Soundcntrl.EnemyAS.PlayOneShot(TempSound.soundtemp.pelletSound);
        Instantiate(GMController.gm.oc.enemypellet, transform.position, Quaternion.Euler(playerDir.transform.eulerAngles));
        shootDelay = .5f;

    }

    IEnumerator ShootPellets()
    {
        attacking = true;
        attackSign.SetActive(true);
        enemyAni.idleStop = true;
        enemyAni.attackStop = false;
        StartCoroutine(enemyAni.AttackAni());
        yield return new WaitForSeconds(attackDelay);
        attackSign.SetActive(false);
        Attack();
        yield return new WaitForSeconds(shootDelay);
        Attack();
        yield return new WaitForSeconds(shootDelay);
        Attack();
        yield return new WaitForSeconds(shootDelay);
        Attack();
        yield return new WaitForSeconds(shootDelay);
        cooldownInt = 0;
        attacking = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, dir.normalized * attackrange);
    }
}
