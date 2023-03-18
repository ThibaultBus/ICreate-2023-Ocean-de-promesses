using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] private Transform prefabRadarPing;
    [SerializeField] private Transform radarPulse;
    [SerializeField] private Transform _transform;
    public float range;
    public float rangeMax = 30f;
    public float rangeSpeed = 20f;

    public float pulseRatio = 40f;
    
    private List<Collider> alreadyDetected = new List<Collider>();

    private void Awake()
    {
    }

    private void FixedUpdate()
    {
        range += rangeSpeed * Time.deltaTime;
        if (range > rangeMax)
        {
            range = 0f;
            alreadyDetected.Clear();
        }

        // we change the scale of the sphere collider for the detection
        // and the scale of the radar pulse for the visual effect
        _transform.localScale = new Vector3(range, range, range);
        float radarPulseRatio = range / pulseRatio;
        radarPulse.localScale = new Vector3(radarPulseRatio, radarPulseRatio, radarPulseRatio);
        
        RaycastHit[] raycastHitList =
            Physics.SphereCastAll(_transform.position, range, Vector3.up, LayerMask.NameToLayer("RadarDetectable"));

        if (raycastHitList.Length > 0)
        {
            foreach (var collision in raycastHitList)
            {
                if (!alreadyDetected.Contains(collision.collider))
                {
                    alreadyDetected.Add(collision.collider);
                    // the position of the radar ping. We need to set all ping in 1f on the y axis, above the radar plan, then they have the same size
                    Vector3 radarPingPosition =
                        new Vector3(collision.transform.position.x, 1f, collision.transform.position.z);
                    Transform radarPingTransform =
                        Instantiate(prefabRadarPing, radarPingPosition, Quaternion.identity);
                    RadarPing radarPing = radarPingTransform.GetComponent<RadarPing>();
                    
                    // we change the color according to the type of the object
                    switch (collision.transform.tag)
                    {
                        case "Fish":
                            radarPing.SetColor(Color.blue);
                            break;
                        case "Zone":
                            radarPing.SetColor(Color.green);
                            break;
                        default:
                            radarPing.SetColor(Color.white);
                            break;
                    }
                    Debug.Log(collision.collider.name);
                }
            }
        }
    }
    
}