using UnityEngine;
using System;
using System.Collections;

public class IKPickUpObject : MonoBehaviour {
    
    protected Animator animator;
    
    public bool ikActive = false;
    public Transform rightHandObj = null;
    public Transform lookObj = null;

    protected float weight;
	protected bool completed = false;

    void Start () 
    {
        animator = GetComponent<Animator>();

        weight = 0;
    }
    
    void Update()
    {
    	if(ikActive)
    	{
    		weight+=0.01f;
    		if(weight>1)
    		{
    			weight = 1;
    		}
    	}
    	else
    	{
    		weight-=0.01f;
    		if(weight<0)
    		{weight = 0;}
    	}
    }
    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if(animator) {  
            
            //if the IK is active, set the position and rotation directly to the goal. 
            if(ikActive) {

                // Set the look target position, if one has been assigned
                if(lookObj != null) {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }    

                // Set the right hand target position and rotation, if one has been assigned
                if(rightHandObj != null && !completed) {
					print ("IKing");
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand,weight);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand,weight);  
                    animator.SetIKPosition(AvatarIKGoal.RightHand,rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandObj.rotation);
					if (weight == 1) {
						ikActive = false;

						lookObj.transform.localPosition = Vector3.zero;
						lookObj.transform.localRotation = Quaternion.identity;
						lookObj.transform.SetParent(animator.GetBoneTransform (HumanBodyBones.RightHand), false);

						weight = 0;
						//lookObj.transform.localScale = Vector3.one;
						//rightHandObj = null;
					}
                }        
                
            }
            
            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else 
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand,weight);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand,weight); 
                animator.SetLookAtWeight(0);
            }
        }
    }    
}