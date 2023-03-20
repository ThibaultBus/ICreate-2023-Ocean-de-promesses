using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantZoneManager : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    [SerializeField] private GameObject _plant;
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        if (_meshRenderer) _meshRenderer.material.color = new Color(1f, 0f, 0f, 0.3f);
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
        if (_meshRenderer) 
            _meshRenderer.material.color = new Color(0f, 1f, 0f, 0.3f);
        
        this.tag = "Untagged";
        Vector3 plantPosition = transform.position;
        plantPosition.y = 0f;
        _plant = Instantiate(_plant, plantPosition, Quaternion.identity);
    }
}
