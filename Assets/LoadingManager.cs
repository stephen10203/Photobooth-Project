using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    IEnumerator request()
    {
        while (true)
        {
            int count = 10;
            www = new WWW("localhost:5000/busy");
            while (count>0)
            {
                count--;
                if (www.isDone)
                {
                    Debug.Log(www.text);
                    if (www.text == "done")
                    {
                        SceneManager.LoadScene("Result");
                    }
                }
                yield return new WaitForSeconds(1);

            }
        }
       
    }
    //string s;
    ////UnityWebRequest wwe;
    WWW www1;
    WWW www;
    public void Start()
    {
        //s = "not received";
        
        PersistentManager pm = GameObject.Find("PersistentManager").GetComponent<PersistentManager>();
        //string filepath = pm.FolderPath();
        string file = pm.unix;
        Debug.Log(file);
        Debug.Log("Button" + pm.buttonNum);
        Debug.Log("localhost:5000/makeGif?file=" + file + "&option=1");
        //UnityWebRequest.Get("localhost:5000/makeGif?file=" + file + "&option=" + pm.buttonNum.ToString());
        www1 = new WWW("localhost:5000/makeGif?file=" + file + "&option=1");
        //wwe= UnityWebRequest.Get("localhost:5000/busy");
        StartCoroutine("request");


    }
    //@"localhost:5000/makeGif?file=1&option=1""
    //// Use this for initialization
    public void Update()
    {
      


    }

}