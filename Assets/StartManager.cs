using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO.Ports;
using System;
using UnityEngine.Networking;

public class StartManager : MonoBehaviour
{
    public void ProceedClick()
    {
        SceneManager.LoadScene("Waiting");
    }

    //byte[] byteSend;
    //SerialPort port;
    //int c;
    ////Boolean checkButton = false;
    //    // Use this for initialization
    //void Start () {
    //   // SceneManager.LoadScene("Waiting");//TEMP
    //    gameObject.transform.Translate(new Vector3(100, 0, 0));
    //    c = 122;
    //    //foreach (string str in SerialPort.GetPortNames())
    //    //{
    //    //    Debug.Log(string.Format("Existing COM port: {0}", str));

    //    //}
    //    string portS = SerialPort.GetPortNames()[0];
    //    Debug.Log(portS);
    //    //port = new SerialPort("COM3", 9600);
    //    port = new SerialPort(portS, 9600);
    //    port.ReadTimeout = 200;
    //    port.Open();
    //    byte[] byteSendStart = Encoding.UTF8.GetBytes("!S-");
    //    //port.Write("!p-");
    //    port.Write(byteSendStart, 0, 3);
    //    port.BaseStream.Flush();
    //    byteSend = Encoding.UTF8.GetBytes("!p-");
    //}
    //// Update is called once per frame
    //void Update () {
    //    port.Write(byteSend, 0, 3);
    //    port.BaseStream.Flush();
    //    c = port.ReadChar();
    //    if (c >= 49 && c <= 53)
    //    {
    //        process();
    //    }

    //}
}