using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Theta2 : MonoBehaviour {
	
	public Transform joint0, joint1;
	Vector3 baseFrame, link1;
	float theta;
	public Transform baseRotation;
	public Text theta2Text;
	
	// sets and displays theta 2 each frame
	void Update () 
	{
		
		//get vector from base along x
		baseFrame = joint0.forward;
		
		//get vector from base to joint 1
		link1 = joint1.position - joint0.position;
		
		//get the angle (in degrees) between the vectors along the links
		theta = Vector3.Angle(baseFrame, link1);
		
		//sets and displays theta
		DHParameters.setTheta2(theta);
		theta2Text.text = ""+theta;
		
	}
	
		
	
}
