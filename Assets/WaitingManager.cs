using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaitingManager : MonoBehaviour
{
    public int timeOutTime;
    public byte[] byteSend;
    public Sprite[] sprites;

    //public SerialPort port;
    public int c;

    private PersistentManager pm;

    // Use this for initialization
    public void Start()
    {
        GameObject.Find("Webcam").GetComponent<WebCam>().Ports();
        pm = GameObject.Find("PersistentManager").GetComponent<PersistentManager>();
        StopAllCoroutines();
        c = 0;
        //Ports();
        timeOutTime = 30;
        Debug.Log(timeOutTime);
        StartCoroutine("Timeout");
    }

    private IEnumerator Timeout()
    {
        while (timeOutTime > 0)
        {
            GameObject.Find("TimeImage").GetComponent<Image>().sprite = sprites[Math.Max(0, timeOutTime - 1)];
            timeOutTime--;

            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene("Start");
        yield return 0;
    }

    // Update is called once per frame
    public void Update()
    {
    }

    private void PhotoButton()
    {
        GameObject.Find("CountdownOverlay").GetComponent<CanvasGroup>().alpha = 0.7f;
        GameObject.Find("Countdown").GetComponent<CanvasGroup>().alpha = 1;
        int unixInt = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        pm.unix = unixInt.ToString();
        Debug.Log(pm.unix);
        //Directory.CreateDirectory("C:/Users/ComputerMania/elrow-booth");
        Directory.CreateDirectory(pm.location() + pm.unix);

        StopCoroutine("Timeout");
        GameObject.Find("Countdown").GetComponent<CountdownScript>().Enabled();
    }

    private void tryTakePhoto()
    {
        //if (c == 122)
        //{
        PhotoButton();
        //}
    }

    public void ButtonPress1()
    {
        pm.buttonNum = 1;
        GameObject.Find("Webcam").GetComponent<WebCam>().button = "1";
        byteSend = Encoding.UTF8.GetBytes(pm.buttonNum.ToString());
        tryTakePhoto();
    }

    public void ButtonPress2()
    {
        pm.buttonNum = 2;
        GameObject.Find("Webcam").GetComponent<WebCam>().button = "2";
        byteSend = Encoding.UTF8.GetBytes(pm.buttonNum.ToString());
        tryTakePhoto();
    }

    public void ButtonPress3()
    {
        pm.buttonNum = 3;
        GameObject.Find("Webcam").GetComponent<WebCam>().button = "3";
        byteSend = Encoding.UTF8.GetBytes(pm.buttonNum.ToString());
        tryTakePhoto();
    }

    public void ButtonPress4()
    {
        pm.buttonNum = 4;
        GameObject.Find("Webcam").GetComponent<WebCam>().button = "4";
        byteSend = Encoding.UTF8.GetBytes(pm.buttonNum.ToString());
        tryTakePhoto();
    }

    public void ButtonPress5()
    {
        pm.buttonNum = 5;
        GameObject.Find("Webcam").GetComponent<WebCam>().button = "5";
        byteSend = Encoding.UTF8.GetBytes(pm.buttonNum.ToString());
        tryTakePhoto();
    }


}