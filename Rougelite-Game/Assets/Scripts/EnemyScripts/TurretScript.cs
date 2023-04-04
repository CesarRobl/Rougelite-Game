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
        SeekPlayer();
        AttackRange();
         if(attack)Attack();
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
