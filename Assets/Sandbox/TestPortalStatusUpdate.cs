using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        if (Input.GetKeyDown("1") || Input.GetKeyDown("2"))
        {
            CheckPortalDestination();
        }
    }

    void CheckPortalDestination()
    {
        bool IsFirstPortal = Input.GetKeyDown("1");
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Portal");
        if (objs.Length == 0)
        {
            Debug.Log("No portals exist");
        }
        int nextRoomId;
        if (IsFirstPortal && objs.Length >= 1 && objs[0].GetComponent<PortalStatus>().GetStatus() == "active")
        {
            nextRoomId = objs[0].GetComponent<PortalStatus>().GetId();
        }
        else if (!IsFirstPortal && objs.Length >= 2 && objs[1].GetComponent<PortalStatus>().GetStatus() == "active")
        {
            nextRoomId = objs[1].GetComponent<PortalStatus>().GetId();
        } else
        {
            return; // No protals active
        }
        RunDataManager.AdvanceToRoomId(nextRoomId);
        SceneManager.LoadSceneAsync((int)SceneIndexes.ROOM_LOADER);
    }
}
