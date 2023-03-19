using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector1 : MonoBehaviour
{
    [SerializeField] private Transform prefabRadarPing;
    [SerializeField] private Transform RadarPulse;
    [SerializeField] private Transform _transform;
    public float range;
    public float rangeMax = 70f; // rayon de la sphère
    public float rangeSpeed = 1f; // uniquement pour le visuel

    public float pulseRatio;
    
    private List<Collider> alreadyDetected = new List<Collider>();

    private void Awake()
    {
        _transform.localScale = new Vector3(rangeMax, rangeMax, rangeMax);
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
        
        // trop demandant en ressources, la sphère va rester stable
        // _transform.localScale = new Vector3(range, range, range);
        float radarPulseRatio = range / pulseRatio;
        RadarPulse.localScale = new Vector3(radarPulseRatio, radarPulseRatio, radarPulseRatio);
        
        RaycastHit[] raycastHitList =
            Physics.SphereCastAll(_transform.position, range, Vector3.up, LayerMask.NameToLayer("RadarDetectable"));

        if (raycastHitList.Length > 0)
        {
            foreach (var collision in raycastHitList)
            {
                if (!alreadyDetected.Contains(collision.collider))
                {
                    alreadyDetected.Add(collision.collider);
                    if ( !(collision.transform.tag == "Fish" ||
                         collision.transform.tag == "PlantZone") )
                    {   
                        continue;
                    }
                    
                    // the position of the radar ping. We need to set all ping in 1f on the y axis, above the radar plan, then they have the same size
                    Vector3 radarPingPosition =
                        new Vector3(collision.transform.position.x, 26f, collision.transform.position.z);
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
                        case "PlantZone":
                            radarPing.SetColor(Color.red);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Ray(_transform.position, GetVectorFromAngle(_transform.eulerAngles.y)));
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        // angle = 0 -> 360
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), 0, -Mathf.Sin(angleRad));
    }
}