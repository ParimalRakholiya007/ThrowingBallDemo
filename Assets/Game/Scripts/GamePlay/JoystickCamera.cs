using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickCamera : MonoBehaviour
{
    public Transform player;
    public float cameraRotSpeed;
    public VariableJoystick variableJoystick;
    public Vector3 offset;

    public void FixedUpdate()
    {
        transform.position = player.position + offset;
        Vector3 roation = new Vector3(0f, variableJoystick.Horizontal, 0f);
        transform.Rotate(roation * cameraRotSpeed * Time.deltaTime);
    }

    private void OnMouseDrag()
    {
        Vector3 roation = new Vector3(variableJoystick.Vertical,0f, 0f);
        transform.Rotate(roation * cameraRotSpeed * Time.deltaTime);
    }
}
