using Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightArduinoTrigger : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<PlayerController>())
        {
            if (ArduinoIO.Instance == null)
                return;
            
            ArduinoIO.Instance.SendOutput(new ArduinoIO.DataOutput(1));
        }
    }
    
    void OnTriggerExit(Collider col)
    {
        if (col.GetComponent<PlayerController>())
        {
            if (ArduinoIO.Instance == null)
                return;
            
            ArduinoIO.Instance.SendOutput(new ArduinoIO.DataOutput(0));
        }
    }
}
