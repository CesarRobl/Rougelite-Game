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
        }
        
        if (attack & !attacking & !anim)
        {
            Attack();
        }
        AttackRange();
        SeekPlayer();
        if(HP <= 0) GMController.gm.Die(gameObject, GetComponent<LootSystem>());
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
        yield return new WaitForSeconds(.2f);
        ai.speed = chargePower;
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
