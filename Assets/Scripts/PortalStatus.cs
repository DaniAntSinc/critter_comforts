using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalStatus : MonoBehaviour
{
    public string destination;
    // Current, absolute state of the portal
    [SerializeField]
    private string status;

    // Start is called before the first frame update
    void Start()
    {
        this.SetStatus("inert");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDestination(string destination)
    {
        this.destination = destination;
    }

    public string GetDestination()
    {
        // TODO Better data structure
        return this.destination;
    }

    public void SetStatus(string status)
    {
        this.status = status;
    }

    // Should be used externally 
    public string GetStatus()
    {
        return this.status;
    }
}
