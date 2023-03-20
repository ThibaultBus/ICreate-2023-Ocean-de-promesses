using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        DroneManager drone = other.GetComponent<DroneManager>();
        if (drone != null)
        {
            drone.EnterZone(this);
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        DroneManager drone = other.GetComponent<DroneManager>();
        if (drone != null)
        {
            drone.ExitZone(this);
        }
    }
}
