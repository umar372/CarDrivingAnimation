using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveAnimControl : MonoBehaviour {

    public GameObject steeringWheel;
    private float angle;

    private Quaternion refSteerRotation;

    // Use this for initialization
    void Start() {
        refSteerRotation = steeringWheel.transform.localRotation;
        angle = refSteerRotation.z;
    }

    // Update is called once per frame
    void Update() {
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

    }

    void ShiftGearDown()
    {

    }

}
