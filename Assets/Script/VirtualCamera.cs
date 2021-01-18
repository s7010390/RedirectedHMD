using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Experimental.Input;
public class VirtualCamera : MonoBehaviour
{
    public Transform Camera;
    public float CameraRotationY;
    public float HumanRotationY;
    public int Direct;
    public float lastValue;
    public int state = 0;
    public int EnableChange = 0;
    public int LastEnableChange = 0;
    public float Gain = 0.5f;
    public float AutoRotateDirection = 0;
    public float y = 0;

    // Start is called before the first frame update
    void OnRotateLeft()
    {
            AutoRotateDirection = 0;
    }
    void OnRotateRight()
    {
           AutoRotateDirection = 1;
    }

    void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        //CameraRotationY = Camera.localEulerAngles.y;
        
        CameraRotationY = AutoRotateConstantSpeed();
        HumanRotationY = CameraRotationY;
        SetDirection();    
        CheckAndUpDate_State();
        RotateCam();            
        Camera.transform.eulerAngles = new Vector3(0,CameraRotationY,0);
        transform.eulerAngles = new Vector3(0f, HumanRotationY, 0.0f); 
        lastValue = CameraRotationY;
    }
    float AutoRotateConstantSpeed()
    {
        if(AutoRotateDirection == 0)
        {
            if(y < 360 && y >0  )
            {
                y += Time.deltaTime * 30;
            }
            else
            {
            y = 0 +Time.deltaTime * 30;
            }
        }
        else if(AutoRotateDirection == 1)
        {
            if(y < 360 && y > 0 )
            {
                y -= Time.deltaTime * 30;
            }
            else
            {
            y = 360 - Time.deltaTime * 30;
            }
        }
        return y;
    }

    void SetDirection()
    {
        float tmp = CameraRotationY - lastValue;
        if(tmp > 0.15  )
        {
           Direct = 1;
        }
        else if(tmp < -0.15)
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
