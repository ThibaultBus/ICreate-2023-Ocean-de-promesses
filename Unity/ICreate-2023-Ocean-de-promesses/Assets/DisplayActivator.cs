using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayActivator : MonoBehaviour
{
    // Start is called before the first frame update
    static bool created = false;
    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        } else {
            Destroy(this.gameObject);
        }
    }
    
    void Start()
    {
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }
}
