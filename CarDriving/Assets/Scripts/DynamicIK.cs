using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicIK : MonoBehaviour {
    // public fields to visualize the value of the Joystick/Keyboard input
    public float vert_val_debug;
    public float horiz_val_debug;

    // public and private fields to control the influence of the IK
    public GameObject ik_target_Le,ik_target_ri;
    public float ikInfluence;
    private float ikInfluenceSpeed = 0.5f;

    // Reference to the animator, on wwhich we will set the value of the parameters and the IK info.
    private Animator anim;

    float angle = 2f;
    // Use this for initialization
    void Start () {
        this.anim = GetComponent<Animator>();
        InvokeRepeating("RotateRightObj",0f,2f);
    }

    // Update is called once per frame
    void Update () {
		
	}

    void RotateRightObj()
    {
        Debug.Log("Calling the func for rotation");
        ik_target_ri.transform.Rotate(new Vector3(angle + 3f, angle+3f, angle + 3f));
    }
    void OnAnimatorIK(int layerIndex)
    {

        Debug.Log ("ik on layer " + layerIndex);
        Vector3 ik_target_posle = this.ik_target_Le.transform.position;
        Vector3 ik_target_posri = this.ik_target_ri.transform.position;

        
        anim.SetIKPosition(AvatarIKGoal.LeftHand, ik_target_posle);
        anim.SetIKPosition(AvatarIKGoal.RightHand, ik_target_posri);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0F);

        anim.SetIKRotation(AvatarIKGoal.RightHand, Quaternion.LookRotation(ik_target_ri.transform.forward));
        float delta_ik_influence = this.ikInfluenceSpeed * Time.deltaTime;
        //anim.set

        if (Input.GetKey(KeyCode.LeftShift))
            {
            Debug.Log("Pressed Shift Key ");

            this.ikInfluence += delta_ik_influence;
            if (this.ikInfluence > 1.0f) this.ikInfluence = 1.0f;

            }
            else
            {
                this.ikInfluence -= delta_ik_influence;
                if (this.ikInfluence < 0.0f) this.ikInfluence = 0.0f;
            }

        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, this.ikInfluence);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, this.ikInfluence);
       

    }
}
