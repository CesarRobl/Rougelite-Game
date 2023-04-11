using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Charger : TestAI
{
    private bool buffer,stopai;
    [SerializeField] private float chargePower;
    private int chargeNum;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (found & !stun & !anim)
        {
           MoveToPlayer();
           SpriteDir(GMController.gm.oc.normalEnemy);
        }
        
        if (attack & !attacking & !anim)
        {
           
            Attack();
        }
        
        
        AttackRange(~(1<<0 | 1<< 2));
        SeekPlayer();
        Enemyhit();
    }

    public override void Attack()
    {
        ai.destination = transform.position;
        attacking = true;
        StartCoroutine(Charge());
        
    }

    IEnumerator Charge()
    {
        anim = true;
        SpriteDir(GMController.gm.oc.normalEnemy);
        yield return new WaitForSeconds(.65f);
        ai.maxSpeed = chargePower;
        ai.destination = GMController.gm.temp.transform.position;
        yield return new WaitForSeconds(.1f);
            attacking = false;
        yield return new WaitForSeconds(2f);
        anim = false;
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, dir.normalized * attackrange);
    }
}
