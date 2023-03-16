using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class ArduinoInputReader : MonoBehaviour
{
    [Serializable] struct InputSettings
    {
        public float xAxisCoeff;
        public float yAxisCoeff;
    }

    [SerializeField] private InputSettings settings;

    private DroneManager _droneManager;
    
    private void Awake()
    {
        _droneManager = GetComponent<DroneManager>();
    }

    public void HandleInput(ArduinoIO.DataInput data)
    {
        Vector2 rawInput = new Vector2(
            data.x * settings.xAxisCoeff,  data.y * settings.yAxisCoeff);
        
        _droneManager.Move(Vector2.ClampMagnitude(rawInput, 1));
        
        // TODO: Handle plant button
    }
}
