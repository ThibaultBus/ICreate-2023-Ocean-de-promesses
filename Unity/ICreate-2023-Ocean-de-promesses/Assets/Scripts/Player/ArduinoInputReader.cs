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
        Vector3 rawInput = new Vector3(
            data.x * settings.xAxisCoeff, 0,  data.y * settings.yAxisCoeff);

        _droneManager.Move(Vector3.ClampMagnitude(rawInput, 1));
        
        // TODO: Handle plant button
        if (data.switchStatus == 1)
        {
            _droneManager.Plant();
        }
    }
}
