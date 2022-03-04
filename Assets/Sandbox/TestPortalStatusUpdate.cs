using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPortalStatusUpdate : MonoBehaviour
{
    public GameObject testPortalInstance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            testPortalInstance.GetComponent<PortalStatus>().SetStatus("active");
        }
        if (Input.GetKeyUp("space"))
        {
            testPortalInstance.GetComponent<PortalStatus>().SetStatus("inert");
        }
        if (Input.GetKeyDown("left"))
        {
            testPortalInstance.GetComponent<PortalStatus>().RerollColor();
        }
    }
}
