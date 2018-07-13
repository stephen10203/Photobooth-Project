using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRKeyboard.Utils;

public class TestingManager : MonoBehaviour {

    public KeyboardManager keyboard;
    // Use this for initialization
    void Start () {
        keyboard = GameObject.Find("Keyboard").GetComponent<KeyboardManager>();

    }

    public void At()
    {
       // keyboard.inputText.text += "@";
    }
}
