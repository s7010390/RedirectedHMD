using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Experimental.Input;
public class VirtualCamera : MonoBehaviour
{
    public Transform Camera;

    public float CameraRotationY ;
    public float HumanRotationY;
    public int Direct;
    public float lastValue;
    public int state = 0;
    public float tmp ;
    int EnableChange = 0;
    int LastEnableChange = 0;
    float Gain = 0.5f;
    public int AutoRotateDirection = 0;
    //float y = 0;
    float Speed = 30 ;
    bool StartState = false;

    // Start is called before the first frame update
    
    void OnRotateLeft()
    {
            AutoRotateDirection = 1;

    }
    void OnRotateRight()
    {
           AutoRotateDirection = 0;

    }
    
    void OnStartProgram()
    {
           if(StartState == false)
           {
                StartState = true;
           }
           else
           {
                StartState = false;
           }
           
    }

    void Start()
    {
       // RandomSpeed();       
        Debug.Log("Speed:" + Speed);
        //CameraRotationY = Camera.localEulerAngles.y ;
        CameraRotationY = 0 + Time.deltaTime * Speed;

    }
    // Update is called once per frame
    void Update()
    {
        
            //Debug.Log(Camera.localEulerAngles.y );
            if(StartState == false)
            {
        
                float a = CameraRotationY;
                CameraRotationY = Camera.localEulerAngles.y;
                HumanRotationY = CameraRotationY;
                SetDirection();    
                CheckAndUpDate_State();
                RotateCam();  
                Camera.transform.eulerAngles = new Vector3(0,CameraRotationY,0);
                transform.eulerAngles = new Vector3(0f, HumanRotationY, 0.0f); 

                lastValue = CameraRotationY;

            }
            else
            {

                AutoRotateConstantSpeed();          
                HumanRotationY = CameraRotationY;
                SetDirection();    
                CheckAndUpDate_State();
                RotateCam();  
                transform.eulerAngles = new Vector3(0f, -HumanRotationY, 0.0f); 
                lastValue = CameraRotationY;

            }
        
    }
    void RandomSpeed()
    {
        int RanD = Random.Range(0,7);
        Speed = Mathf.Pow(2,RanD)/10;
    }
    void AutoRotateConstantSpeed()
    {
        if(AutoRotateDirection == 0)
        {
            if(CameraRotationY < 360 && CameraRotationY >0  )
            {
                CameraRotationY += Time.deltaTime * Speed;
            }
            else
            {
                CameraRotationY = 0 +Time.deltaTime * Speed;
            }
        }
        else if(AutoRotateDirection == 1)
        {
            if(CameraRotationY < 360 && CameraRotationY > 0 )
            {
                CameraRotationY -= Time.deltaTime * Speed;
            }
            else
            {
            CameraRotationY = 360 - Time.deltaTime * Speed;
            }
        }
    }

    void SetDirection()
    {
        tmp = CameraRotationY - lastValue;

        if(tmp > 0.003  )
        {
           Direct = 1;
        }
        else if(tmp < -0.003 )
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

            if( HumanRotationY < 90  && LastEnableChange != 1)
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
