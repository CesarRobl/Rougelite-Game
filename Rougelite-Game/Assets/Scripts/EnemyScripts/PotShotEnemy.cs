using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotShotEnemy : TestAI
{
    private bool attacking;
    private Vector2 currentpos;
    void Awake()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        currentpos = transform.position;
        SeekPlayer();
        AttackRange();
        if (found & !stun & !attack & !attacking)
        {
            Debug.Log("Move to player");
            MoveToPlayer();
        }
       
        if(attack & !attacking)Attack();
    }

    public override void Attack()
    {
        attacking = true;
        Vector3 pos = GMController.gm.temp.transform.position;
        Vector2 newpos = new Vector2(Random.Range(pos.x - Random.Range(5,10), pos.x + Random.Range(5,10)), Random.Range(pos.y - Random.Range(5,10), pos.y + Random.Range(5,10)));
        ai.destination = newpos;
        ai.speed = 8;
        if (currentpos == newpos) attacking = false;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, dir.normalized * attackrange);
    }
}
