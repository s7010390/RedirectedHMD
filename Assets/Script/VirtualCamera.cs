using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
    public Transform Camera;
    public float CameraRotationY;
    public float HumanRotationY;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CameraRotationY = Camera.localEulerAngles.y;
        HumanRotationY = - CameraRotationY * 0.5f;

        transform.eulerAngles = new Vector3(0f, HumanRotationY, 0.0f);
    }
}
