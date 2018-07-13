using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
 
public class StreamVideo : MonoBehaviour
{
    //public VideoSource videoSource;
    public VideoClip theVideo;
    // Use this for initialization
    public void Start()
    {
        theVideo = Resources.Load((Application.dataPath) + "/Gif/gif.ogv") as VideoClip;
        GameObject.Find("Example").GetComponent<VideoPlayer>().clip = theVideo;
        Application.OpenURL((Application.dataPath) + "/Folder/File.Type");
    }
}