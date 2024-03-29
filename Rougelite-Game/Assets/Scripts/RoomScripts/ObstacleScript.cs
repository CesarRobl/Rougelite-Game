using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleScript : MonoBehaviour
{
    public int HP;
    [SerializeField] private BoxCollider2D box;
    private CircleCollider2D cc;
    private SpriteRenderer _sprite;
    public Sprite destroyed;
    private float tableChance;
    public List<ObstacleList> obs  = new List<ObstacleList>();
    private bool stop;
    private HurtFunction ow;
    void Awake()
    {
        cc = GetComponent<CircleCollider2D>();
        _sprite = GetComponent<SpriteRenderer>();
        ow = GetComponent<HurtFunction>();
        ChangeSprite();
    }

    
    void Update()
    {
        if (ow.hurt)
        {
            HP--;
            if(HP > 0) SoundControl.Soundcntrl.EffectAS.PlayOneShot(TempSound.soundtemp.tableChairs[1]);
            StartCoroutine(HitParticle());
            ow.hurt = false;
        }
       if(HP <= 0 & !stop) DestroyedSprite();
       
    }
    
    

    // Chooses which sprite to use
    ObstacleList SelectSprite()
    {
        int ran = Random.Range(1, 101);
        List<ObstacleList> sprites = new List<ObstacleList>();
        foreach (var i in obs)
        {
            if(ran <= i.chance ) sprites.Add(i);
           
        }
      
        if (sprites.Count > 1)
        {
            if (sprites[0].chance < sprites[1].chance)
            {
                ObstacleList selectsprite = sprites[0];
                return selectsprite;
            }
            else
            {
                ObstacleList selectsprite = sprites[1];
                return selectsprite;
            }
        }
        
         if (sprites.Count > 0 & sprites.Count < 2)
        {
            ObstacleList selectsprite = sprites[0];
            return selectsprite;
        }
        Debug.Log("You fucked up");
        return null;
    }

    // Changes the sprite to the selected sprite
    void ChangeSprite()
    {
        ObstacleList selectsprite = SelectSprite();
        _sprite.sprite = selectsprite.sprite;
        destroyed = selectsprite.spriteAlt;
    }

    void DestroyedSprite()
    {
        if(GMController.gm.loading)SoundControl.Soundcntrl.EffectAS.PlayOneShot(TempSound.soundtemp.tableChairs[0]);
        _sprite.sprite = destroyed;
        stop = true;
        CircleCollider2D circle = GetComponent<CircleCollider2D>();
        box.enabled = false;
        circle.enabled = false;
    }

   

    public IEnumerator HitParticle()
    {
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        ps.Play();
        yield return new WaitForSeconds(.1f);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Debug.Log(col.gameObject.name);
        // if (col.gameObject.CompareTag("Sword"))
        // {
        //    Debug.Log("Collision");
        //     CheckCollision();
        //     if (playerhit)
        //     {
        //         HP--;
        //         playerhit = false;
        //     }
        // }
    }
 
     void OnTriggerEnter2D(Collider2D other)
    {
       
        // if (other.gameObject.CompareTag("Sword"))
        // {
        //     Debug.Log("Trigger");
        //     HP--;
        // }
    }
}
