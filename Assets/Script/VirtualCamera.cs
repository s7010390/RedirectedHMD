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
        

        if( EnableChange == 1 && (CameraRotationY > 358 || CameraRotationY <= 2))
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
        if(CameraRotationY > 2 && CameraRotationY <= 358 )
        {
            EnableChange = 1;
        }

        //Camera rotate
        if( state == 1  && (CameraRotationY > 2 && CameraRotationY <= 358)) 
        {
            HumanRotationY = 360 - ( 180 - CameraRotationY*0.5f );
            HumanRotationY = -HumanRotationY;
        }
        else if(state == 0) {
            HumanRotationY = -CameraRotationY*0.5f ;


        }
       if(CameraRotationY > 2 && CameraRotationY <= 358)
        transform.eulerAngles = new Vector3(0f, HumanRotationY, 0.0f);
        lastValue = CameraRotationY;
        
        
   
    }
}
