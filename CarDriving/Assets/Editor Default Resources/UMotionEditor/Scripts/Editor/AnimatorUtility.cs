using UnityEngine;
using System.Collections;

namespace UMotionEditor
{
	public static class AnimatorUtility
	{
		//********************************************************************************
		// Public Properties
		//********************************************************************************

        public static HumanBodyBones LastBone
        {
            get
            {
                return HumanBodyBones.LastBone;
            }
        }

        public static bool SkeletonBoneParentImplemented
        {
            get
            {
                #if UNITY_5_5_OR_NEWER
                return true;
                #else
                return false;
                #endif
            }
        }

		//********************************************************************************
		// Private Properties
		//********************************************************************************
		
		//----------------------
		// Inspector
		//----------------------
		
		//----------------------
		// Internal
		//----------------------

		//********************************************************************************
		// Public Methods
		//********************************************************************************
		
        public static void AnimatorStop(Animator animator)
        {
            #if UNITY_5_6_OR_NEWER
            animator.enabled = false;
            animator.enabled = true;
            #else
            animator.Stop();
            #endif
        }

        public static void AnimatorRebindIfRequired(Animator animator)
        {
            if ((animator != null) && !animator.isInitialized)
            {
                animator.Rebind();
            }
        }

		//********************************************************************************
		// Private Methods
		//********************************************************************************
		
	}
}
