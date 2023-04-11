using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    private SpriteRenderer sr;
    private TempPlayer temp;
    public GameObject spatula;

    [SerializeField] private Sprite[] chardir;
    [SerializeField] private Sprite[] attackDir;
    private int num;
    void Start()
    {
        sr = GetComponentInParent<SpriteRenderer>();
        temp = GetComponentInParent<TempPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GMController.gm.ani.attacking)ChangeSpriteDir();
        else Debug.Log("Choose attack sprite " + num);
    }
    
    // changes the player sprite based on the direction its looking at using its z rotation
    void ChangeSpriteDir()
    {

        if (GMController.gm.holder.eulerAngles.z >225 && GMController.gm.holder.eulerAngles.z<=315)
        {
            num = 0;
        }
        
        // else if (GMController.gm.holder.eulerAngles.z <= 45 || GMController.gm.holder.eulerAngles.z > 315)
        // {
        //     Debug.Log("FaceRight");
        // }
        // else if(GMController.gm.holder.eulerAngles.z>135&&GMController.gm.holder.eulerAngles.z<=225)
        //     Debug.Log("FaceLeft");
        
        else if (GMController.gm.holder.eulerAngles.z >45&&GMController.gm.holder.eulerAngles.z<=135)
        {
            num = 1;
        }
        
        sr.sprite = chardir[num];
    }

    
}
