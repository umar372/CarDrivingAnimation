using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoutHandMovement : MonoBehaviour {

    public Transform target,targetSteering;
    public float speed;

    public float TimerCount;
    bool isTimer,isMovingBack;

    public DynamicIK dynIK_ref;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;

        if (DriveAnimControl.isShouting)
        {

            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            if (this.transform.position == target.transform.position)
            {
                isTimer = true;
            }

            if (isTimer)
            {
                transform.localPosition = new Vector3(Mathf.PingPong(Time.time, 0.5f), transform.position.y, transform.position.z);
                TimeCalculate();
            }
        }

        if (isMovingBack)
        {

            transform.position = Vector3.MoveTowards(transform.position, targetSteering.position, 2*Time.deltaTime);
            if (transform.position == targetSteering.transform.position)
            {
                isMovingBack = false;
            }
        }



    }

    void TimeCalculate()
    {
        TimerCount += Time.deltaTime;
        if (TimerCount > 2f)
        { 
            isTimer = false;
            DriveAnimControl.isShouting = false;
            DriveAnimControl.isBackFromShout = true;
            DriveAnimControl.isShoutOnLeft = false;
            isMovingBack = true;
            TimerCount = 0f;

        }
    }
}
