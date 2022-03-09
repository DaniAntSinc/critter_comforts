using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMapGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting Run Generation...");
        if (!RunDataManager.HasExistingRun())
        {
            RunDataManager.BeginRun();
        }
        Debug.Log("Resolved Run Generation.");
        Debug.Log("Printing map:");
        Debug.Log(RunDataManager.GetSchematicsForCurrentRoom().ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
