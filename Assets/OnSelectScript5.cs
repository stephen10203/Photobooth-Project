using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VRKeyboard.Utils;

public class OnSelectScript5 : MonoBehaviour, ISelectHandler
{
    //Do this when the selectable UI object is selected.
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(this.gameObject.name + " was selected");
        DataCollectionManager dm = GameObject.Find("DataCollectionManager").GetComponent<DataCollectionManager>();
        dm.step = 4;
        GameObject.Find("Input").GetComponent<Text>().text = dm.Textboxes[dm.step].text;



    }
}