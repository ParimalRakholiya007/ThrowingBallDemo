using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    private int cameraRotSpeed = 100;

    void Update()
    {
        transform.position = player.position + offset;

        Vector3 v3 = new Vector3(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), 0.0f);
        transform.Rotate(v3 * cameraRotSpeed * Time.deltaTime);
    }
}
