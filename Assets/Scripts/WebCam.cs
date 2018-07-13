using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO.Ports;
using System.Text;

public class WebCam : MonoBehaviour
{
    public IEnumerator Ticking()
    {
        while (count < 10)
        {
            SaveImage(count);
            count++;

            yield return new WaitForSeconds(0.5f);//Delay between camera shots
        }
        button = "-1";
        count = 0;
        c = 0;
        Disabled();
        yield return 0;
    }
    //SerialPort port;
    //SerialPort port2;
    public float delay;
    private WebCamTexture otherTex = null;
    private int count = 0;
    int c;
    byte[] byteSend;
    public string button;

    public void Start()
    {
        delay = 0.5f;
        button = "-1";
        DontDestroyOnLoad(gameObject);
        c = 0;
        count = 0;
        WebCamDevice[] devices = WebCamTexture.devices;

        // for debugging purposes, prints available devices to the console
        for (int i = 0; i < devices.Length; i++)
        {
            print("Webcam available: " + devices[i].name);
        }

        Renderer rend = this.GetComponentInChildren<Renderer>();

        // assuming the first available WebCam is desired
        WebCamTexture tex = new WebCamTexture(devices[0].name);
        rend.material.mainTexture = tex;
        tex.Play();

        otherTex = tex;
    }

    public void Update()
    {
        //if (button !="-1")
        //{
        //    byteSend = Encoding.UTF8.GetBytes(button);
        //    port.Write(byteSend, 0, 1);
        //    port.BaseStream.Flush();
        //    port2.Write(byteSend, 0, 1);
        //    port2.BaseStream.Flush();
        //    c = port.ReadChar();
        //Debug.Log(c);
        //}

        if (SceneManager.GetActiveScene().name == "Waiting")
        {
            gameObject.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }
        //This is to take the picture, save it and stop capturing the camera image.
    }

    public void Disabled()
    {
        StopCoroutine("Ticking");
        SceneManager.LoadScene("Loading");
    }
    public void Enabled()
    {
        StartCoroutine("Ticking");
    }

    private void SaveImage(int i)
    {
        //Create a Texture2D with the size of the rendered image on the screen.
        Texture2D texture = new Texture2D(otherTex.width, otherTex.height, TextureFormat.ARGB32, false);

        //Save the image to the Texture2D
        texture.SetPixels(otherTex.GetPixels());
        texture.Apply();

        //Encode it as a PNG.
        byte[] bytes = texture.EncodeToPNG();

        //Save it in a file.
        //change to save gif as video
        PersistentManager pm = GameObject.Find("PersistentManager").GetComponent<PersistentManager>();
        string extension = "00" + i.ToString() + ".png";
        File.WriteAllBytes(pm.FolderPath() + extension, bytes); //Change save location here.
    }
    public void Ports()
    {
        //string portS = SerialPort.GetPortNames()[0];
        //port = new SerialPort(portS, 9600);
        //port.ReadTimeout = 200;
        //port.Open();
        //string port2S = SerialPort.GetPortNames()[1];
        //port2 = new SerialPort(port2S, 9600);
        //port2.ReadTimeout = 200;
        //port2.Open();
    }
}