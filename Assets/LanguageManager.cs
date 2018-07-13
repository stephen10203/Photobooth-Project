using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VRKeyboard.Utils;

public class LanguageManager : MonoBehaviour
{
    [Serializable]
    public class MyClass
    {
        public string Country;
        public string Event;
        public string Date;
    }
    public string character;
    public bool Caps;
    private bool languageSelected;
    public Text[] fields;
    public Text[] overlays;
    public int step;
    public string language;

    // Use this for initialization
    void Awake()
    {
        Process foo = new Process();
        foo.StartInfo.FileName = "startServer.bat";
        //    //string path = @"..\startServer.bat";
        //    //string path = "@" + s;
        //    //UnityEngine.Debug.Log(path);
        PersistentManager pm = GameObject.Find("PersistentManager").GetComponent<PersistentManager>();
        //string path = pm.location()+@"startServer.bat";
        //string path = System.IO.Directory.GetCurrentDirectory() + @"\startServer.bat";
        //foo.StartInfo.Arguments = path;
        foo.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        foo.Start();
        //    //foo.CloseMainWindow();
        //    //foo.WaitForExit();
        //    //int ExitCode = foo.ExitCode;
        //    //print(ExitCode);
   
    }

    public void Start()
    {
        languageSelected = false;
        language = "";
        step = 0;
    }

    // Update is called once per frame
    public void Update()
    {
        if (languageSelected && step >= 2)
        {
            GameObject.Find("Done").GetComponent<Button>().interactable = true;
        }
        //if (GameObject.Find("Input").GetComponent<Text>().text != "")
        //{
        //    overlays[step].text = "";
        //}
        //fields[step].text = GameObject.Find("Input").GetComponent<Text>().text;
    }

    public void Done()
    {
        PersistentManager pm = GameObject.Find("PersistentManager").GetComponent<PersistentManager>();
        pm.UpdateLanguage(language);
        MyClass myObject = new MyClass();
        myObject.Country = fields[0].text;
        myObject.Event = fields[1].text;
        myObject.Country = fields[2].text;
        string json = JsonUtility.ToJson(myObject) + "\r\n";
        Directory.CreateDirectory(pm.location());
        File.AppendAllText(pm.location() + fields[1].text + ".json", json);
        SceneManager.LoadScene("Start");
    }

    public void Next()
    {
        if (step < 2)
        {
            step += 1;
            fields[step].text = "";
            if (step == 2)
            {
                GameObject.Find("Next").GetComponent<Button>().interactable = false;
            }
        }
    }

    public void English()
    {
        language = "english";
        languageSelected = true;
    }

    public void CapsPress()
    {
        Caps = !Caps;
    }
    //Keyboard Buttons
    public void ClearPress()
    {
        fields[step].text="";
    }
    public void A()
    {
        character = "a";
        if (Caps)
        {
            character = character.ToUpper();
        }
        //GameObject.Find("Input").GetComponent<Text>().text += character;
        fields[step].text = fields[step].text+character;
    }
}