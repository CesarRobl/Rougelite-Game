using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : TestAI
{
    [SerializeField] private float shootDelay;
    [SerializeField] private GameObject playerDir;

    void Awake()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        playerDir.GetComponent<PlayerDirFinder>().PlayerDir();
        SeekPlayer();
        AttackRange(~(1<<0 | 1<< 2));
        if (attack)
        {
            Attack();
            AttackDir(GMController.gm.oc.shooter);
        }
         Enemyhit();
    }

    public override void Attack()
    {
        
        if (shootDelay <= 0)
        {
            Instantiate(GMController.gm.oc.enemypellet, transform.position, Quaternion.Euler(playerDir.transform.eulerAngles));
            shootDelay = .5f;
        }
        else shootDelay -= Time.deltaTime;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, dir.normalized * attackrange);
    }
}
