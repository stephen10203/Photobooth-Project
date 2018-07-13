using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteScript : MonoBehaviour {

    Image myImageComponent;
    public Sprite myFirstImage; //Drag your first sprite here in inspector.
    public Sprite mySecondImage; //Drag your second sprite here in inspector.

    public void Start() //Lets start by getting a reference to our image component.
    {
        myImageComponent = GetComponent<Image>();
        SetImage1();
    }

    public void SetImage1() //method to set our first image
    {
        myImageComponent.sprite = myFirstImage;
    }

    public void SetImage2()
    {
        myImageComponent.sprite = mySecondImage;
    }
}
