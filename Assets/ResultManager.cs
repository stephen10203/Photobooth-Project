using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    public IEnumerator TimeOutTimer()
    {
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }

    // Use this for initialization
    private int timeLeft = 30;

    public void Start()
    {
        StartCoroutine("TimeOutTimer");
    }

    // Update is called once per frame
    public void Update()
    {
        if (timeLeft == 0)
        {
            StopAllCoroutines();
            SceneManager.LoadScene("Start");
        }
    }

    public void EmailButton()
    {
        SceneManager.LoadScene("DataCollection");
    }

    public void RetakePhoto()
    {
        SceneManager.LoadScene("Waiting");
    }

    public void CancelPhoto()
    {
        SceneManager.LoadScene("Start");
    }
}