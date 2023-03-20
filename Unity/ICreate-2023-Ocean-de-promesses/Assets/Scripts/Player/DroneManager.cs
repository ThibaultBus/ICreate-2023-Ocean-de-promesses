using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerController))]
public class DroneManager : MonoBehaviour
{
    private ZoneManager _currentZone = null;
    private PlantZoneManager _currentPlantZone = null;
    
    private PlayerController _playerController;

    private float lastInputTime;
    [SerializeField] private float inputResetCooldown = 8f;
    
    private float _lastPlantTime = 0f;
    
    [SerializeField] private float plantCooldown = 1f;
    
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    // Reset the game if the player is inactive for too long
    public void Start()
    {
        lastInputTime = 0f;
    }


    public void Update()
    {
        Debug.Log(lastInputTime + "    " + Time.deltaTime);
        lastInputTime += Time.deltaTime;
        if (lastInputTime > inputResetCooldown)
        {
            SceneManager.LoadScene("Menu");
        }
    }

    /* Actions */
    public void Move(Vector3 movement)
    {
        _playerController.MovementInput = movement;
        if (movement != Vector3.zero)
        {
            lastInputTime = 0f;
        }
        Debug.Log("INPUT : " + lastInputTime);
    }
    
    public void Plant()
    {
        if (_currentPlantZone != null && Time.time - _lastPlantTime > plantCooldown)
        {
            _currentPlantZone.Plant();
        }
        
        lastInputTime = 0f;
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
