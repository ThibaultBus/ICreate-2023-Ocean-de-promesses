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
    
    private float _lastPlantTime = 0f;
    
    [SerializeField] private float plantCooldown = 1f;
    
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }
    
    /* Actions */
    public void Move(Vector3 movement)
    {
        _playerController.MovementInput = movement;
    }
    
    public void Plant()
    {
        if (_currentPlantZone != null && Time.time - _lastPlantTime > plantCooldown)
        {
            _currentPlantZone.Plant();
            _lastPlantTime = Time.time;
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
