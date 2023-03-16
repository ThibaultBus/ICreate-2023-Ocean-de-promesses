using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
    private ZoneManager _currentZone = null;
    
    public void EnterZone(ZoneManager zone)
    {
        _currentZone = zone;
        
        // Display text depending on zone
    }
    
    public void ExitZone(ZoneManager zone)
    {
        // only if (_currentZone == zone) ?
        _currentZone = null;

        // Display/Hide text depending on zone
    }
}
