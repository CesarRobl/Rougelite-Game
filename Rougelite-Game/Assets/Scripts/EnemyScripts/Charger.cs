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
        
        if (found & !stun & !attack & !buffer)
        {
            ai.maxSpeed = speed;
            ai.destination = GMController.gm.temp.transform.position;
            stopai = false;
        }
        
        if (attack & !buffer)
        {
            if (!stopai)
            {
                ai.destination = transform.position;
                stopai = true;
            }
            Attack();
        }
        AttackRange();
        SeekPlayer();
        if(HP <= 0) GMController.gm.Die(gameObject, GetComponent<LootSystem>());
    }

    public override void Attack()
    {
        buffer = true;
        StartCoroutine(Charge());
        
    }

    IEnumerator Charge()
    {
        yield return new WaitForSeconds(.2f);
        ai.speed = chargePower;
        ai.destination = GMController.gm.temp.transform.position;
            yield return new WaitForSeconds(1f);
            buffer = false;
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, dir.normalized * attackrange);
    }
}
