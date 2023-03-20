using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{

    public void HandleInput(ArduinoIO.DataInput data)
    {
        if (data.switchStatus == 1)
        {
            Debug.Log("start button pressed");
            SceneManager.LoadScene("SampleScene");
        }
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
