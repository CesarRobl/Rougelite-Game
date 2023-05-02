using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField]private ESpritesStorage _eSprite;
    public SpriteRenderer sprite;
    public PlayerDirFinder dir;
    public int spriteDir;
    public float delay,attackDelay;
    private bool _stop, _stopSwitch;
    public bool switchOnce;
    [HideInInspector] private int _idleNum, _attackNum, _walkNum;
    [HideInInspector] public bool idleStop,attackStop, walkStop;
    [HideInInspector] public bool idlePlaying,attackPlaying, walkPlaying;
    [HideInInspector] public GameObject slashes;
    [HideInInspector] public Vector3[] _rot;
    void Awake()
    {
       
    }

    
    void Update()
    {
        spriteDir = dir.spriteNum;
    }

    void SwitchWalk()
    {
        switch (spriteDir)
        {
            case 0:
            {
                _eSprite.walk = _eSprite.walkBack;
                break;
            }
            case 1:
            {
                _eSprite.walk = _eSprite.walkFront;
                break;
            }
            case 2:
            {
                _eSprite.walk = _eSprite.walkLeft;
                break;
            }
            case 3:
            {
                _eSprite.walk = _eSprite.walkRight;
                break;
            }
        }
    }
    
    public void SwitchAttack()
    {
        if (!_stopSwitch)
        {
            switch (spriteDir)
            {
                case 0:
                {
                    _eSprite.attack = _eSprite.attackBack;
                    break;
                }
                case 1:
                {
                    _eSprite.attack = _eSprite.attackFront;
                    break;
                }
                case 2:
                {
                    _eSprite.attack = _eSprite.attackLeft;
                    break;
                }
                case 3:
                {
                    _eSprite.attack = _eSprite.attackRight;
                    break;
                }
            }

            if (switchOnce) _stopSwitch = true;
        }
    }
   

    public IEnumerator Idle()
    {
        if (!idleStop)
        {
           
            _idleNum++;
            if (_idleNum > _eSprite.idleSprite.Length - 1) _idleNum = 0;

            yield return new WaitForSeconds(delay);
            sprite.sprite = _eSprite.idleSprite[_idleNum];
            StartCoroutine(LoopIdle());
        }
    }

    IEnumerator LoopIdle()
    {
        yield return new WaitForSeconds(.1f);
        StartCoroutine(Idle());
    }
    
    public IEnumerator WalkAni( )
    {
        if (!walkStop)
        {
          
            walkPlaying = true;
            SwitchWalk();
            _walkNum++;
            if (_walkNum > _eSprite.walk.Length - 1) _walkNum = 0;

            yield return new WaitForSeconds(delay);
            sprite.sprite = _eSprite.walk[_walkNum];
            StartCoroutine(LoopWalk());
        }
    }
    
    IEnumerator LoopWalk()
    {
        yield return new WaitForSeconds(.1f);
        StartCoroutine(WalkAni());
    }
    
    public IEnumerator AttackAni()
    {
       
        if (!attackStop)
        {
           
            attackPlaying = true;
            SwitchAttack();
            _attackNum++;
            if (_attackNum > _eSprite.attack.Length - 1) _attackNum = 0;
        
            yield return new WaitForSeconds(attackDelay);
            sprite.sprite = _eSprite.attack[_attackNum];
            StartCoroutine(LoopAttack());
        }
    }
    
    IEnumerator LoopAttack()
    {
        yield return new WaitForSeconds(.1f);
        StartCoroutine(AttackAni());
    }
    
    public IEnumerator PhantomSlash(Animator n)
    {

      
       
        if (!attackStop)
        {
            if (!_stop)
            {
                _attackNum = 0;
                _stop = true;
            }
           
            attackPlaying = true;
            SwitchAttack();
            _attackNum++;
            if (_attackNum > _eSprite.attack.Length - 1) _attackNum = 0;
            
            yield return new WaitForSeconds(attackDelay);

            if (_attackNum == _eSprite.attack.Length - 1 || _attackNum == (_eSprite.attack.Length / 2 ) - 1)
            {
                slashes.transform.eulerAngles = _rot[dir.spriteNum];
                SoundControl.Soundcntrl.EnemyAS.PlayOneShot(TempSound.soundtemp.swordclips[0]);
                n.SetTrigger("Slash");
            }
            sprite.sprite = _eSprite.attack[_attackNum];
            StartCoroutine(LoopSlash(n));
        }
    }
    
    IEnumerator LoopSlash(Animator n)
    {
        yield return new WaitForSeconds(.1f);
        StartCoroutine(PhantomSlash(n));
    }

    public void StopAttack()
    {
        attackStop = true;
        walkStop = false;
        _stop = false;
        _stopSwitch = false;
    }

    public void StopSlash()
    {
        attackStop = true;
        walkStop = false;
        _stop = false;
    }
}
