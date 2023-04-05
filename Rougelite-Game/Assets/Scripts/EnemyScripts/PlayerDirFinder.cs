using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirFinder : MonoBehaviour
{
    private SpriteRenderer enemySprite;
    
    void Start()
    {
        enemySprite = GetComponentInParent<SpriteRenderer>();
    }

   
    void Update()
    {
       
    }

    public void ChangeSprite(float z, Sprite[] sprites, SpriteRenderer esprite)
    {
        // Debug.Log("My rotation is at " + z );
        if ( z > 225 && z <= 315)
        {
            Debug.Log("Facing foward");
            esprite.sprite = sprites[1];
        }
        // else if (z <= 45 || z > 315)
        // {
        //     Debug.Log("FaceRight");
        // }
        // else if( z > 135 && z<=225)
        //     Debug.Log("FaceLeft");
        else if ( z >45 && z<=135)
        {
            Debug.Log("Facing back");
            esprite.sprite = sprites[0];
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
