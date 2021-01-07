using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
    public Transform Camera;
    public float CameraRotationX;
    public float CameraRotationY;
    public float CameraRotationZ;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // CameraRotationX = Camera.eulerAngles.x;
        CameraRotationY = Camera.localEulerAngles.y;

        // transform.rotation = Quaternion.Euler(0f, CameraRotationY * 2, 0f);
        // CameraRotationZ = Camera.eulerAngles.z;

        transform.eulerAngles = new Vector3(0f, -CameraRotationY + CameraRotationY * 0.5f, 0.0f);
    }
}
