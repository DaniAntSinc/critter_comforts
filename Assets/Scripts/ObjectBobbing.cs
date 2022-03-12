using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBobbing : MonoBehaviour
{
    public float speedUpDown = 1;
    public float distanceUpDown = 1;

    public Vector3 offset;

    void Update()
    {
        Vector3 mov = new Vector3(transform.position.x, Mathf.Sin(speedUpDown * Time.time) * distanceUpDown, transform.position.z);
        transform.position = mov + offset;
    }
}
