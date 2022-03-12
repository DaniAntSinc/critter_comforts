using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    public Transform objectToLookAt;
    void Update()
    {
        transform.LookAt(objectToLookAt);
    }
}
