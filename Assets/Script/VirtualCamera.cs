using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
    public Transform Camera;
    public float CameraRotationY;
    public float HumanRotationY;
    public int Direct;
    public float lastValue;

    public int state;
    public int EnableChange = 1;
    public float Gain = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CameraRotationY = Camera.localEulerAngles.y;
        HumanRotationY = CameraRotationY;
        //check Direction
        if(CameraRotationY > lastValue  )
        {
           Direct = 1;
        }
        else
        {
           Direct = 2;
        }
        //checkstate
        

        if( EnableChange == 1 && (CameraRotationY > 355 || CameraRotationY <= 5))
        {   
            if(Direct == 1  && state == 0 )
            {
                state = 1;
                 EnableChange = 0;
            }
            else if(Direct == 2  && state == 0)
            {
                state = 1;
                 EnableChange = 0;
            }
            else if(Direct == 1 && state == 1)
            {
                state = 0;
                 EnableChange = 0;
            }
            else if(Direct == 2 && state == 1)
            {
                state = 0;
                 EnableChange = 0;
            }
        
        }
        if(CameraRotationY > 5 && CameraRotationY <= 355 )
        {
            EnableChange = 1;
        }

        //Camera rotate
        if(CameraRotationY > 5 && CameraRotationY <= 355)
        {
            if( state == 1 ) 
            {
                HumanRotationY = 360 - ( 180 - CameraRotationY*Gain );
                HumanRotationY = -HumanRotationY;
            }
            else 
            {
                HumanRotationY = -CameraRotationY*Gain ;
            }
           
            transform.eulerAngles = new Vector3(0f, HumanRotationY, 0.0f);
        }
        
        lastValue = CameraRotationY;
        
        
   
    }
}
