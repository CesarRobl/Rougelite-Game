using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : TestAI
{

    [SerializeField] private float shootDelay;
    [SerializeField] private GameObject playerDir;
    private Vector2 dir;

    

    // Update is called once per frame
    void Awake()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        SeekPlayer();
        AttackRange();
        // if(attack)Attack();
        // else if(found & !attack) MoveToPlayer();
        if (found)
        {
           Debug.Log(ai.maxSpeed);
            ai.maxSpeed = speed;
            ai.destination = GMController.gm.temp.transform.position;
        }
    }

    public override void Attack()
    {
        if (shootDelay <= 0)
        {
            Instantiate(GMController.gm.oc.enemypellet, transform.position, Quaternion.Euler(playerDir.transform.eulerAngles));
            shootDelay = 2f;
        }
        else shootDelay -= Time.deltaTime;
        if(HP <= 0) GMController.gm.Die(gameObject, GetComponent<LootSystem>());
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, dir.normalized * attackrange);
    }
   

   
}
