using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : TestAI
{
    [SerializeField] private float shootDelay;
    private float pastDelay;
    [SerializeField] private GameObject playerDir;
    void Awake()
    {
        Setup();
        pastDelay = shootDelay;
    }

    
    void Update()
    {
        SeekPlayer();
        AttackRange();
        if(found & !attack)MoveToPlayer();
        else if(attack) Attack();
        if(HP <= 0) GMController.gm.Die(gameObject, GetComponent<LootSystem>());
    }


    public override void Attack()
    {
        ai.destination = transform.position;
        if (shootDelay <= 0)
        {
            Instantiate(GMController.gm.oc.enemypellet, transform.position, Quaternion.Euler(playerDir.transform.eulerAngles));
            shootDelay = pastDelay;
        }
        else shootDelay -= Time.deltaTime;
    }
}
