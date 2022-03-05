using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPortalInstantiation : MonoBehaviour
{
    public GameObject portalPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 basePosition = new Vector3(-6.92f, 2.13f, -8f);
        Quaternion baseRotation = Quaternion.Euler(0f, 90f, 0f);
        string[] destination = new string[] { "swamp", "desert", "volcano", "forest", "mystery" };
        // Create a portal for each biome    
        for (int i = 0; i < 2; i++)
        {
            GameObject instance = Instantiate(portalPrefab, basePosition, baseRotation);
            instance.GetComponent<PortalStatus>().SetDestination(destination[i]);
            basePosition.z += 9;
        }
        // Create a portal for each biome    
        basePosition = new Vector3(9f, 2.13f, -11.27f);
        baseRotation = Quaternion.Euler(0f, 0f, 0f);
        for (int i = 2; i < 5; i++)
        {
            GameObject instance = Instantiate(portalPrefab, basePosition, baseRotation);
            instance.GetComponent<PortalStatus>().SetDestination(destination[i]);
            basePosition.x -= 6;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
