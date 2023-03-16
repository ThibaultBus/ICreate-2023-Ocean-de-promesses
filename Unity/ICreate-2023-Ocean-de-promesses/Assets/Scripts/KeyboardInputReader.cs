﻿using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DroneManager))]
public class KeyboardInputReader : MonoBehaviour
{
    private DroneManager _droneManager;
    
    private void Awake()
    {
        _droneManager = GetComponent<DroneManager>();
    }

    public void Update()
    {
        Vector2 rawInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        _droneManager.Move(Vector2.ClampMagnitude(rawInput, 1));

        if (Input.GetButtonDown("Fire1"))
        {
            _droneManager.Plant();
        }
    }
}
