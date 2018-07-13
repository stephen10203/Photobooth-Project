using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sprite = Resources.Load("Images/image", typeof(Sprite)) as Sprite;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
