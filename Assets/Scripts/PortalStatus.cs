using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalStatus : MonoBehaviour
{
    private string destination;
    private int id;
    // Current, absolute state of the portal
    [SerializeField] private string status;

    // Start is called before the first frame update
    void Start()
    {
        this.SetStatus("inert");
    }

    public void SetDestination(int id, string destination)
    {
        this.id = id;
        this.destination = destination;
    }

    public string GetDestination()
    {
        // TODO Better data structure
        return this.destination;
    }

    public int GetId()
    {
        return this.id;
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
