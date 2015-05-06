using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpperArmSlider : MonoBehaviour {
	
	//These slots are where you will plug in the appropriate robot parts into the inspector.
	public Transform RobotBase;
	public Transform RobotUpperArm;
	
	public Transform joint0, joint1, joint2;
	Vector3 link1, link2;
	float theta, lastTheta;
	
	public Slider sliderTheta3;
	
	void Start()
	{
		//get vector from joint 0 to 1
		link1 = joint1.position - joint0.position;
		
		//get vector from joint 1 to 2
		link2 = joint2.position - joint1.position;
		
		//get the angle (in degrees) between the vectors along the links
		theta = Vector3.Angle(link1, link2);
		
		//give angle correct sign
		if((AngleDir(link1, link2,Quaternion.Euler(0, -90, 0) * link1) < 0f) )//&& (baseRotation.rotation.eulerAngles.y > 180))
		{
			theta*=-1;
		}
		
		lastTheta = theta;
		sliderTheta3.value = theta;
		
		
	}

	//handles slider during automation
	void Update () 
	{
		if(DHParameters.getMoveSlider())
		{
			sliderTheta3.value = DHParameters.getTheta3();
			lastTheta = DHParameters.getTheta3();
		}
	}
	
	//determines which side of the previous vector the current vector falls on (used to determine which sign should be used)
	float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up) 
	{
		Vector3 perp = Vector3.Cross(fwd, targetDir);
		float dir = Vector3.Dot(perp, up);
		
		if (dir > 0f) 
		{
			return 1f;
		} 
		else if (dir < 0f) 
		{
			return -1f;
		} 
		else
		{
			return 0f;
		}
	}
	
	//called when theta3 slider is moved.  rotates upper arm
	public void SliderJoint2(float angle)
	{
		if(DHParameters.getMoveSlider() == false)
		{
			//RobotUpperArm.rotation = Quaternion.Euler(-(angle+84.7248f), RobotUpperArm.rotation.y, RobotUpperArm.rotation.z);
			RobotUpperArm.Rotate (-(angle - lastTheta), 0f , 0f);
			lastTheta = angle;
		}
		
	}
	
	
}
