using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Experimental.Input;
public class VirtualCamera : MonoBehaviour
{
    public Transform Camera;
    public GameObject CavasText;

    public float CameraRotationY;
    public float HumanRotationY;
    public int Direct;
    public float lastValue;
    public int state = 0;
    int EnableChange = 0;
    int LastEnableChange = 0;
    float Gain = 0.5f;
    float AutoRotateDirection = 0;
    float y = 0;
    float Speed ;
    bool StartState = false;
    // Start is called before the first frame update
    void OnRotateLeft()
    {
            AutoRotateDirection = 0;
    }
    void OnRotateRight()
    {
           AutoRotateDirection = 1;
 
    }
    void OnStartProgram()
    {
           StartState = true;
           CavasText.gameObject.SetActive(false);
    }

    void Start()
    {
        RandomSpeed();       
        Debug.Log("Speed:" + Speed);

    }
    // Update is called once per frame
    void Update()
    {
        if(StartState == true)
        {
            //CameraRotationY = Camera.localEulerAngles.y;
            CameraRotationY = AutoRotateConstantSpeed();
            HumanRotationY = CameraRotationY;
            SetDirection();    
            CheckAndUpDate_State();
            RotateCam();            
            transform.eulerAngles = new Vector3(0f, HumanRotationY, 0.0f); 
            Camera.transform.eulerAngles = new Vector3(0,CameraRotationY,0);

            lastValue = CameraRotationY;
        }
    }
    void RandomSpeed()
    {
        int RanD = Random.Range(0,7);
        Speed = Mathf.Pow(2,RanD)/10;
    }
    float AutoRotateConstantSpeed()
    {
        if(AutoRotateDirection == 0)
        {
            if(y < 360 && y >0  )
            {
                y += Time.deltaTime * Speed;
            }
            else
            {
            y = 0 +Time.deltaTime * Speed;
            }
        }
        else if(AutoRotateDirection == 1)
        {
            if(y < 360 && y > 0 )
            {
                y -= Time.deltaTime * Speed;
            }
            else
            {
            y = 360 - Time.deltaTime * Speed;
            }
        }
        return y;
    }

    void SetDirection()
    {
        float tmp = CameraRotationY - lastValue;
        if(tmp > 0  )
        {
           Direct = 1;
        }
        else if(tmp < 0 )
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
