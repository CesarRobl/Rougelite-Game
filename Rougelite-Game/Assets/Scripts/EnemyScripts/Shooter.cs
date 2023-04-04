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
        
        Debug.Log("My z is at " + movementDir.PlayerDir());
        SeekPlayer();
        AttackRange();
        if (found & !attack)
        {
            MoveToPlayer();
           SpriteDir(GMController.gm.oc.shooter);
        }
        else if (attack)
        {
            Attack();
            AttackDir(GMController.gm.oc.shooter);
        }
        Enemyhit();
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
