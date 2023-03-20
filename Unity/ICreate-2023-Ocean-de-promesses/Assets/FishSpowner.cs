using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpowner : MonoBehaviour
{
    public GameObject MeshAsset;
    public List<GameObject> objectsList = new List<GameObject>();
    //public GameObject FishAsset;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 terrainCenter = MeshAsset.transform.position ;
        int numObjects = 7;
        //float radius = MeshAsset.transform.localScale.x / 2f;
        float slices = MeshAsset.transform.localScale.x / (numObjects);
        float sizeZ = MeshAsset.transform.localScale.z / 2;
        foreach (GameObject FishAsset in objectsList)
        {

            // Instantiate objects in a circle around the terrain center
            for (int i = 0; i < numObjects; i++)
            {
                // Calculate the angle between objects in the circle
                //float angle = i * Mathf.PI * 2f / numObjects;
                
                // Calculate the position of the object using the angle and radius
                Vector3 spawnPosition = new Vector3(i * slices, 3f, Random.Range(0f, 1f) * sizeZ) + terrainCenter;
                //Debug.Log(spawnPosition);

                // Instantiate the object at the spawn position
                Instantiate(FishAsset, spawnPosition, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

/*for (int i = 0; i < numObjects; i++)
           {
               // Calculate the angle between objects in the circle

               // Calculate the position of the object using the angle and radius
               Vector3 spawnPosition = new Vector3(Random.Range(-1,1) * radius, 10f, Random.Range(-1, 1) * radius) + terrainCenter;
               Debug.Log(spawnPosition);

               // Instantiate the object at the spawn position
               Instantiate(FishAsset, spawnPosition, Quaternion.identity);
           }*/