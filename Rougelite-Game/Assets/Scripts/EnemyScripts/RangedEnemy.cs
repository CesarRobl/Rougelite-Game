using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : TestAI
{

    [SerializeField] private float shootdelay;
    [SerializeField] private bool stop,stunned;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private GameObject PlayerDir;
    private Vector2 dir;

    void Awake()
    {
        
        ps = GetComponentInChildren<ParticleSystem>();
        pastpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        dir = GMController.gm.temp.transform.position - transform.position;
        SeekPlayer();
        AttackRange();
        if (found) RangedAI();
        if (HP <= 0) GMController.gm.Die(gameObject,GetComponent<LootSystem>());
    }

    public void RangedAI()
    {
        if(!attack)MoveToPlayer();
        else ShootPlayer();
    }

    void MoveToPlayer()
    {
        ai.maxSpeed = speed;
        ai.destination = GMController.gm.temp.transform.position;
    }

   public void ShootPlayer()
   {
       ai.destination = transform.position;
        if (shootdelay <= 0)
        {
            Instantiate(GMController.gm.oc.enemypellet, transform.position, Quaternion.Euler(PlayerDir.transform.eulerAngles));
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
            
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            HP--;
            if (stop == false)
            {
                stop = true;
                StartCoroutine(EnemyHurt(ps));
                Knockback(GMController.gm.maxforce);
            }
        }
    }

    public void Knockback(float force)
    {
        rb.AddForce(-dir.normalized * GMController.gm.maxforce, ForceMode2D.Impulse);
        stunned = true;
        StartCoroutine(Reset());
        stop = false;
    }
    
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(GMController.gm.forcedelay);
        rb.velocity = Vector2.zero;
        stunned = false;
    }

}
