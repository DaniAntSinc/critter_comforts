using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject playerModel;

    public float speed = .1f;

    void Update()
    {
        float xDirection = Input.GetAxis("Horizontal");
        float zDirection = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(-xDirection, 0.0f, -zDirection);

        transform.position += moveDirection * speed;

        if (moveDirection != Vector3.zero)
        {
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, Quaternion.LookRotation(new Vector3(xDirection, 0.0f, zDirection)), Time.deltaTime * 100);
        }
    }
}
