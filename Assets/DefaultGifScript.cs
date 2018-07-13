using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGifScript : MonoBehaviour {

    public Sprite theSprite;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<SpriteRenderer>().sprite = theSprite;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
