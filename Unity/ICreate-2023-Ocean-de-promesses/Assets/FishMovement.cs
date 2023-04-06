using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public GameObject theTerrain;
    public float FishSpeed;

    private void Start()
    {

    }
    private void Update()
    {

        //Debug.Log(transform.rotation);
        //Debug.Log(Quaternion.Angle(transform.rotation, Quaternion.Euler(0f, 180f, 0f)));
        if (Quaternion.Angle(transform.rotation, Quaternion.Euler(0f, 0f, 0f)) < 1)
        {
            if (transform.position.z < theTerrain.transform.position.z + theTerrain.transform.localScale.z)
            {
                transform.position += new Vector3(0f, 0f, FishSpeed);
            }

            else
            {
                //transform.position -= new Vector3(10f, 0f, 0f);
                transform.Rotate(new Vector3(180f, 0f, 180f));
            }
        }
        else if (Quaternion.Angle(transform.rotation, Quaternion.Euler(180f, 0f, 180f)) < 1)
        {
            if (transform.position.z > theTerrain.transform.position.z)
            {
                transform.position -= new Vector3(0f, 0f, FishSpeed);
                //Debug.Log(transform.position);
            }
            else
            {
                //transform.position += new Vector3(1f, 0f, 0f);
                transform.Rotate(new Vector3(180f, 0f, 180f));
            }
        }
    }
}


/*
private void Update()
{

    //Debug.Log(transform.rotation);
    //Debug.Log(Quaternion.Angle(transform.rotation, Quaternion.Euler(0f, 180f, 0f)));
    if (Quaternion.Angle(transform.rotation, Quaternion.Euler(180f, 0f, 180f)) < 1)
    {
        if (transform.position.z < theTerrain.transform.position.z + theTerrain.transform.localScale.z)
        {
            transform.position += new Vector3(0f, 0f, FishSpeed);
        }

        else
        {
            //transform.position -= new Vector3(10f, 0f, 0f);
            transform.Rotate(new Vector3(180f, 0f, 180f));
        }
    }
    else if (Quaternion.Angle(transform.rotation, Quaternion.Euler(0f, 0f, 0f)) < 1)
    {
        if (transform.position.z > theTerrain.transform.position.z)
        {
            transform.position -= new Vector3(0f, 0f, FishSpeed);
            //Debug.Log(transform.position);
        }
        else
        {
            //transform.position += new Vector3(1f, 0f, 0f);
            transform.Rotate(new Vector3(180f, 0f, 180f));
        }
    }
}
}*/