using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveAnimControl : MonoBehaviour {

    public GameObject steeringWheel;

    private float angle;
    private Quaternion refSteerRotation;


    //Root object for Gear lever to rotate the whole lever.
    public GameObject gearRoot;

    public bool isGearUp, isGearDown,doUp,doDown;
    private float gearShiftSpeed;

    public DynamicIK dynIk_ref;

    // Use this for initialization
    void Start() {
        gearShiftSpeed = 100f;
        refSteerRotation = steeringWheel.transform.localRotation;
        angle = refSteerRotation.z;
    }

    // Update is called once per frame
    void Update() {

        if (isGearUp || Input.GetKey(KeyCode.RightControl))
        {
            doUp = true;
            doDown = false;
            
        }
        else if (isGearDown || Input.GetKey(KeyCode.RightAlt))
        {
            doDown = true;
            doUp = false;
        }

        if (doUp)
        {
            ShiftGearUp();
            dynIk_ref.ri_targ_ind = 1;

        }
        else {
            dynIk_ref.ri_targ_ind = 0;
        }

        if (doDown)
        {
            ShiftGearDown();
            dynIk_ref.ri_targ_ind = 0;
            dynIk_ref.ri_targ_ind = 1;

        }


        if (Input.GetKey(KeyCode.RightArrow))
        {
            SteerRight();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            SteerLeft();
        }
    }


    void SteerRight()
    {
        Debug.Log("Steering Right");
        steeringWheel.transform.Rotate(0f, 0f, 1f);
    }

    void SteerLeft()
    {
        Debug.Log("Steering Left");
        steeringWheel.transform.Rotate(0f, 0f, -1f);
    }

    void ShiftGearUp()
    {

        //Gear Up shifting Till it reaches the desire rotation level 
        //As soon as it reaches there make DoUp bool false so it stop 
        // Calling this functon from update method
        Debug.Log("Euler Angles "+gearRoot.transform.rotation.z);

        if (gearRoot.transform.rotation.z > -0.13f)
            gearRoot.transform.Rotate(0f, 0f, -1f * gearShiftSpeed * Time.deltaTime);
        else
            doUp = false;
    }

    void ShiftGearDown()
    {
        //Doing the Rotation backwards for gear lever. 
        //If lever reaches to desired rotation make boolean doDown false so 
        //It doesn't continue more rotation on z-Axis
        if (gearRoot.transform.rotation.z < 0.13f)
            gearRoot.transform.Rotate(0f, 0f, 1f * gearShiftSpeed * Time.deltaTime);
        else
            doDown = false;
    }

}
