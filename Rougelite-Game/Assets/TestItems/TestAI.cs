using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAI : MonoBehaviour
{
    public int HP;
    public bool found;
    public float drange;
    
    [HideInInspector]public Vector3 pastpos;
    private Vector2 dir;
    [SerializeField] private Rigidbody2D RB;
    [SerializeField] private bool Stop;
    private void Awake()
    {
        pastpos= transform.position;
    }

    void Update()
    {
       
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
         dir = GMController.gm.player.position - transform.position;
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        TempPlayer tp = col.gameObject.GetComponent<TempPlayer>();
        if (tp != null & !GMController.gm.playerhurt)
        {
            GMController.gm.playerhealth--;
            GMController.gm.playerhurt = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            //HP--;
            if (Stop == false)
            {
                Stop = true;
                Knockback(GMController.gm.maxforce);
            }
        }
    }

    public void Knockback(float force)
    {
        Debug.Log("force");
        RB.AddForce(-dir.normalized * GMController.gm.maxforce, ForceMode2D.Force);
        Stop = false;
        
    }
}
