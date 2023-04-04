using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    private SpriteRenderer sr;
    private TempPlayer temp;
    public GameObject spatula;

    [SerializeField] private Sprite[] chardir;
    void Start()
    {
        sr = GetComponentInParent<SpriteRenderer>();
        temp = GetComponentInParent<TempPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSpriteDir();
    }
    
    // changes the player sprite based on the direciton its looking at using its z rotation
    void ChangeSpriteDir()
    {

        if (GMController.gm.holder.eulerAngles.z >225 && GMController.gm.holder.eulerAngles.z<=315)
        {
            sr.sprite = chardir[0];
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
            sr.sprite = chardir[1];
        }
    }
}
