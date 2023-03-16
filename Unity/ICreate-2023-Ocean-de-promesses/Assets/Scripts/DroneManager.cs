using Player;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class DroneManager : MonoBehaviour
{
    private ZoneManager _currentZone = null;
    private PlantZoneManager _currentPlantZone = null;
    
    private PlayerController _playerController;
    
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }
    
    /* Actions */
    public void Move(Vector2 movement)
    {
        _playerController.MovementInput = movement;
    }
    
    public void Plant()
    {
        if (_currentPlantZone != null)
        {
            _currentPlantZone.Plant();
        }
    }

    /* Zone management */
    public void EnterZone(ZoneManager zone)
    {
        _currentZone = zone;
        
        // Display text depending on zone
        Debug.Log("Entered zone: " + zone.name + "");
    }
    
    public void ExitZone(ZoneManager zone)
    {
        if (_currentZone == zone)
            _currentZone = null;
        
        // Display/Hide text depending on zone
        Debug.Log("Exited zone: " + zone.name + "");
    }
    
    public void EnterPlantZone(PlantZoneManager zone)
    {
        _currentPlantZone = zone;
        
        // Display text depending on zone
        Debug.Log("Entered zone: " + zone.name + "");
    }
    
    public void ExitPlantZone(PlantZoneManager zone)
    {
        if (_currentPlantZone == zone)
            _currentPlantZone = null;
        
        // Display/Hide text depending on zone
        Debug.Log("Exited zone: " + zone.name + "");
    }
}
