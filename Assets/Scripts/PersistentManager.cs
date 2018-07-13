using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net;
using System.IO.Ports;
using UnityEngine.UI;

public class PersistentManager : MonoBehaviour
{
    public static PersistentManager Instance { get; private set; }

    public Text text;
    public Dictionary<words, string> Dic;
    public Dictionary<words, string> English;
    public string currentLanguage;

    public string unix;
    public int buttonNum;



    // Use this for initialization
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        Debug.Log("STUFF");
        English = new Dictionary<words, string>();
        fillEnglish();
        Dic = new Dictionary<words, string>();
        unix = "";
        currentLanguage = "english";
    }

    public enum words
    {
        A,
        B,
        C,
        D,
        E
    };

    private void fillEnglish()
    {
        English.Add(words.A, "Animal");
    }

    public void UpdateLanguage(string language)
    {
        currentLanguage = language.Trim().ToLower();
        switch (language)
        {
            case "english":
                Dic = English;
                break;

            default:
                break;
        }
    }

    public string lang(words word)
    {
        return Dic[word];
    }

    public string location()
    {
        //Debug.Log(System.IO.Directory.GetCurrentDirectory() + @"\Emails\");
        return System.IO.Directory.GetCurrentDirectory() + @"\Emails\";
        //Debug.Log(@"C:\Users\ComputerMania\elrow - booth\");
        //return @"C:\Users\ComputerMania\elrow - booth\";


    }

    public string FolderPath()
    {
        string FolderName = unix + @"\";
        string MainFolder = location();
        return MainFolder + FolderName;//+File;
    }

}