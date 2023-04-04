using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirFinder : MonoBehaviour
{
    private SpriteRenderer enemySprite;
    [SerializeField] private Sprite[] sprites;
    void Start()
    {
        enemySprite = GetComponentInParent<SpriteRenderer>();
    }

   
    void Update()
    {
       
    }

    public void ChangeSprite(float z)
    {
        if (GMController.gm.holder.eulerAngles.z >225 && GMController.gm.holder.eulerAngles.z<=315)
        {
            enemySprite.sprite = sprites[0];
            //spatula.transform.position = new Vector3(spatula.transform.position.x, spatula.transform.position.y, -.5f);
        }
        else if (GMController.gm.holder.eulerAngles.z <= 45 || GMController.gm.holder.eulerAngles.z > 315)
        {
            Debug.Log("FaceRight");
        }
        else if(GMController.gm.holder.eulerAngles.z>135&&GMController.gm.holder.eulerAngles.z<=225)
            Debug.Log("FaceLeft");
        else if (GMController.gm.holder.eulerAngles.z >45&&GMController.gm.holder.eulerAngles.z<=135)
        {
            enemySprite.sprite = sprites[1];
        }
    }

    public void PlayerDir()
    {
        Vector3 pos = GMController.gm.temp.transform.position;
        Vector2 dir = pos - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f * Time.deltaTime);
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
