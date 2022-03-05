using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPortalStatusUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            PortalStatus[] portalList = FindObjectsOfType<PortalStatus>();
            foreach (PortalStatus portal in portalList)
            {
                portal.SetStatus("active");
            }
        }
        if (Input.GetKeyUp("space"))
        {
            PortalStatus[] portalList = FindObjectsOfType<PortalStatus>();
            foreach (PortalStatus portal in portalList)
            {
                portal.SetStatus("inert");
            }
        }
    }
}
