using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : TestAI
{

    [SerializeField] private float shootdelay;
    [SerializeField] private bool Stop;
    [SerializeField] private Rigidbody2D RB;
    private Vector2 dir;

    void Awake()
    {
        pastpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        dir = GMController.gm.temp.transform.position - transform.position;
        SeekPlayer();
        if (found) RangedAI();
        else transform.position = pastpos;
        if (HP <= 0) GMController.gm.Die(gameObject);
    }

    void RangedAI()
    {
        MoveToPlayer();
        ShootPlayer();
    }

    void MoveToPlayer()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, GMController.gm.player.position, .5f * Time.deltaTime);
    }

    void ShootPlayer()
    {
        if (shootdelay <= 0)
        {
            Instantiate(GMController.gm.oc.enemypellet, transform.position, Quaternion.identity);
            shootdelay = 2;
        }
        else shootdelay -= Time.deltaTime;
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
