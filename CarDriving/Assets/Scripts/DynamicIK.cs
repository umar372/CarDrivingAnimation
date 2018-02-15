using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicIK : MonoBehaviour {
    // public fields to visualize the value of the Joystick/Keyboard input
    public float vert_val_debug;
    public float horiz_val_debug;

    // public and private fields to control the influence of the IK
    public GameObject[] ik_target_Le;
    public GameObject[] ik_target_ri;
    public float ikInfluence;


    public int ri_targ_ind,le_targ_ind;
    private float ikInfluenceSpeed = 0.5f;

    public Quaternion rotationOfHand;

    // Reference to the animator, on which we will set the value of the parameters and the IK info.
    private Animator anim;
    float angle = 2f;
    
    // Use this for initialization
    void Start () {
        rotationOfHand = new Quaternion();
        ri_targ_ind = 0;
        le_targ_ind = 0;
        this.anim = GetComponent<Animator>();
        InvokeRepeating("RotateRightObj",0f,2f);
    }

    // Update is called once per frame
    void Update () {
	    	
	}

    
    void OnAnimatorIK(int layerIndex)
    {

        Debug.Log ("ik on layer " + layerIndex);
        Vector3 ik_target_posle = this.ik_target_Le[le_targ_ind].transform.position;
        Vector3 ik_target_posri = this.ik_target_ri[ri_targ_ind].transform.position;

        
        anim.SetIKPosition(AvatarIKGoal.LeftHand, ik_target_posle);
        anim.SetIKPosition(AvatarIKGoal.RightHand, ik_target_posri);


        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0F);
        Debug.Log("Local Rotation " + ik_target_ri[ri_targ_ind].transform.rotation);
        anim.SetIKRotation(AvatarIKGoal.RightHand, ik_target_ri[ri_targ_ind].transform.rotation);


        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0F);
        Debug.Log("Local Rotation " + ik_target_Le[0].transform.rotation);
        anim.SetIKRotation(AvatarIKGoal.LeftHand, ik_target_Le[0].transform.rotation);

        float delta_ik_influence = this.ikInfluenceSpeed * Time.deltaTime;

        this.ikInfluence += delta_ik_influence;
        if (this.ikInfluence > 1.0f) this.ikInfluence = 1.0f;
        
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, this.ikInfluence);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, this.ikInfluence);
       

    }
}
