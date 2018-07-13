using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountdownScript : MonoBehaviour
{
    private IEnumerator Time()
    {
        while (timeLeft>0)
        {
            gameObject.GetComponent<Image>().sprite = spriteArray[Math.Max(0, timeLeft - 1)];
            timeLeft--;
            // Debug.Log(timeLeft);

            yield return new WaitForSeconds(1);
        }
        GameObject.Find("Webcam").GetComponent<WebCam>().Enabled();
        yield return 0;
    }

    public Sprite[] spriteArray;

    public int timeLeft;

    public void Start()
    {
        StopCoroutine("Time");
        timeLeft = 5;
        Debug.Log(timeLeft);
    }

    public void Update()
    {
        if (timeLeft <= 0)
        {
            
        }
    }
    public void Enabled()
    {
        timeLeft = 5;
        StartCoroutine("Time");
    }
}