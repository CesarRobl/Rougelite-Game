using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirFinder : MonoBehaviour
{
    private SpriteRenderer enemySprite;
    [HideInInspector] public int spriteNum;
    void Start()
    {
        enemySprite = GetComponentInParent<SpriteRenderer>();
    }

   
    void Update()
    {
       
    }

    public void SpriteStateChange(Sprite sprite, SpriteRenderer esprite)
    {
        esprite.sprite = sprite;
    }
    public void ChangeSprite(float z)
    {
        // Debug.Log("My rotation is at " + z );
        if ( z > 225 && z <= 315)
        {
            //face down
            spriteNum = 1;
           
        }
        else if (z <= 45 || z > 315)
        {
            // face right
            spriteNum = 3;
            
        }
        else if (z > 135 && z <= 225)
        {
            // face left
            spriteNum = 2;
            
        }
            
        else if ( z >45 && z<=135)
        {
           // face up
            spriteNum = 0;
            
        }
    }

    public void GetSpriteNum()
    {
        ChangeSprite(PlayerDir());
    }

    public float PlayerDir()
    {
        Vector3 pos = GMController.gm.temp.transform.position;
        Vector2 dir = pos - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp( transform.rotation, rotation, 10f * Time.deltaTime);
        float z = transform.eulerAngles.z;
        return z;
    }

    public float Dir(Vector3 pos)
    {
      
        Vector2 dir = pos - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp( transform.rotation, rotation, 10f * Time.deltaTime);
        return  transform.eulerAngles.z;
    }
}
