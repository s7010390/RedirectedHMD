using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
    public Transform Camera;
    public float CameraRotationY;
    public float HumanRotationY;
    public int Direct;
    public int lastValue;

    public int state = 0;
    public int EnableChange = 0;
    public int LastEnableChange = 0;

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
   
        SetDirection();    

        CheckAndUpDate_State();
        RotateCam();
        transform.eulerAngles = new Vector3(0f, HumanRotationY, 0.0f); 
        lastValue = (int)CameraRotationY;
    
    }

    void SetDirection()
    {
        int tmp = (int)CameraRotationY - lastValue;
        if(tmp > 1  )
        {
           Direct = 1;
        }
        else if(tmp < -1)
        {
           Direct = 2;
        }
    }
    void CheckAndUpDate_State()
    {

        if(Direct == 1 ){
            if( HumanRotationY < 90 && LastEnableChange != 1)
            {
                state += 1;
                EnableChange = 1;
                Debug.Log(state);
            }
            else if(HumanRotationY < 180 && HumanRotationY >= 90 && LastEnableChange != 2)
            {  
                state += 1;
                EnableChange = 2;
            }
            else if(HumanRotationY < 270 && HumanRotationY >= 180 && LastEnableChange != 3)
            {
                state += 1;
                EnableChange = 3;
            }
            else if(HumanRotationY < 360 && HumanRotationY >= 270 && LastEnableChange != 4)
            {                       
                state += 1;
                EnableChange = 4;
                if( state == -2)
                {
                    state = 2;
                }

             }          
        }
        else  if(Direct == 2){

            if( HumanRotationY < 90 && LastEnableChange != 1)
            {
                state -= 1;
                EnableChange = 1;
                if( state == 5)
                {
                        state = 1;
                }
            }
            else if(HumanRotationY < 180 && HumanRotationY >= 90 && LastEnableChange != 2)
            {
                state -= 1;
                EnableChange = 2;
            }
            else if(HumanRotationY < 270 && HumanRotationY >= 180 && LastEnableChange != 3)
            {
                state -= 1;
                EnableChange = 3;
            }
            else if(HumanRotationY < 360 && HumanRotationY >= 270 && LastEnableChange != 4)
            {
                state -= 1;
                EnableChange = 4;
            }    
        }        
        LastEnableChange = EnableChange;
    }
    void RotateCam()
    {
        if( state ==  EnableChange + 2 || state ==  EnableChange - 2) 
            {
                HumanRotationY = 360 - ( 180 - CameraRotationY*Gain ); //Gain calculation
                HumanRotationY = -HumanRotationY;
            }
        else 
        {
                HumanRotationY = -CameraRotationY*Gain ;    //Gain calculation
        }
    }
}
