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
    public void ChangeSprite(float z, Sprite[] sprites, SpriteRenderer esprite)
    {
        // Debug.Log("My rotation is at " + z );
        if ( z > 225 && z <= 315)
        {
            
            spriteNum = 1;
            esprite.sprite = sprites[spriteNum];
        }
        // else if (z <= 45 || z > 315)
        // {
        //     Debug.Log("FaceRight");
        // }
        // else if( z > 135 && z<=225)
        //     Debug.Log("FaceLeft");
        else if ( z >45 && z<=135)
        {
           
            spriteNum = 0;
            esprite.sprite = sprites[spriteNum];
        }
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
