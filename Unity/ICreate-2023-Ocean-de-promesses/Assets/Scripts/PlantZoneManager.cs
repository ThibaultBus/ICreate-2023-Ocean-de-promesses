using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class PlantZoneManager : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material.color = new Color(1f, 0f, 0f, 0.3f);
    }
    
    public void OnTriggerEnter(Collider other)
    {
        DroneManager drone = other.GetComponent<DroneManager>();
        if (drone != null)
        {
            drone.EnterPlantZone(this);
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        DroneManager drone = other.GetComponent<DroneManager>();
        if (drone != null)
        {
            drone.ExitPlantZone(this);
        }
    }
    
    public void Plant()
    {
        _meshRenderer.material.color = new Color(0f, 1f, 0f, 0.3f);
    }
}
