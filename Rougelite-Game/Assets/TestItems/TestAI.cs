using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAI : MonoBehaviour
{
    public int HP;
    public bool found;
    public float drange;

    private Vector3 pastpos;
    private void Start()
    {
        pastpos= transform.position;
    }

    void Update()
    {
        Debug.Log(drange);
        if (found) MoveToPlayer();
        else transform.position = pastpos;
      SeekPlayer();
        if(HP <= 0) GMController.gm.Die(gameObject);
        
        
    }

    void MoveToPlayer()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, GMController.gm.player.position, 2 * Time.deltaTime);
    }

    void SeekPlayer()
    {
        Vector2 dir = GMController.gm.player.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, drange, ~LayerMask.NameToLayer("Pellet"));
        if (hit.collider != null)
        {
            
            TempPlayer pc = hit.collider.GetComponent<TempPlayer>();
            if (pc != null)
            {
                found = true;
            }
            else found = false;
        }
    }

   

    
}
