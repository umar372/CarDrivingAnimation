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

    public static bool isShoutOnLeft,isShouting,isBackFromShout;
    public GameObject lookMirrorLeft, lookMirrorRight, lookShout, lookCenter;

    float mir_look_timer;
    bool is_mirror_look;
    // Use this for initialization
    void Start() {
        gearShiftSpeed = 60f;
        refSteerRotation = steeringWheel.transform.localRotation;
        angle = refSteerRotation.z;
    }

    // Update is called once per frame
    void Update() {

        if (!doUp && !doDown)
        {
            if (Input.GetKey(KeyCode.RightShift))
            {
                isShoutOnLeft = true;

            }

            if (isShoutOnLeft)
            {
                if (!isShouting)
                    ShoutOnLeftSide();
            }

            if (isBackFromShout)
            {
                isShoutOnLeft = false;
                dynIk_ref.le_targ_ind = 0;
                isBackFromShout = false;
                dynIk_ref.isLookingTowards = false;

            }
        }

        if (!isShouting)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                doUp = true;
                doDown = false;

            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                doDown = true;
                doUp = false;
            }

            if (doUp)
            {
                ShiftGearUp();
                dynIk_ref.ri_targ_ind = 1;

            }
            else
            {
                dynIk_ref.ri_targ_ind = 0;
            }

            if (doDown)
            {
                ShiftGearDown();
                dynIk_ref.ri_targ_ind = 0;
                dynIk_ref.ri_targ_ind = 1;

            }

            if (Input.GetKey(KeyCode.A))
            {
                dynIk_ref.isLookingTowards = true;
                dynIk_ref.lookIndex = 1;
                is_mirror_look = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                dynIk_ref.isLookingTowards = true;
                dynIk_ref.lookIndex = 2;
                is_mirror_look = true;
            }

        }

        if (is_mirror_look)
        {
            mir_look_timer += Time.deltaTime;
            if (mir_look_timer > 1.5f)
            {
                dynIk_ref.isLookingTowards = false;
               // dynIk_ref.lookIndex = 0;
                is_mirror_look = false;
                mir_look_timer = 0f;

            }
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


    public void SteerRight()
    {
       // Debug.Log("Steering Right"+ steeringWheel.transform.rotation.z);
        
        if(steeringWheel.transform.rotation.z <0.1)
            steeringWheel.transform.Rotate(0f, 0f, 1f);
    }

    public void SteerLeft()
    {
        Debug.Log("Steering Left"+ steeringWheel.transform.rotation.z);
        if (steeringWheel.transform.rotation.z > -0.34f)
            steeringWheel.transform.Rotate(0f, 0f, -1f);
    }

    public void ShiftGearUp()
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

    public void ShiftGearDown()
    {
        //Doing the Rotation backwards for gear lever. 
        //If lever reaches to desired rotation make boolean doDown false so 
        //It doesn't continue more rotation on z-Axis
        if (gearRoot.transform.rotation.z < 0.13f)
            gearRoot.transform.Rotate(0f, 0f, 1f * gearShiftSpeed * Time.deltaTime);
        else
            doDown = false;
    }

    public void ShoutOnLeftSide()
    {
        if (!isShouting)
        {
            dynIk_ref.le_targ_ind = 1;
            isShouting = true;
            //dynIk_ref.LookPoint(0.5f,lookShout);
            dynIk_ref.isLookingTowards = true;
            dynIk_ref.lookIndex = 3;
        }
    }

}
