using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoLists : MonoBehaviour
{

    [HideInInspector] public VideoPlayer video;
    public VideoClip[] clips;

     void Start()
     {
         video = GetComponent<VideoPlayer>();
     }
}
