using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LowerArmSlider : MonoBehaviour {
	
	//These slots are where you will plug in the appropriate robot parts into the inspector
	public Transform RobotBase;
	public Transform RobotLowerArm;
	
	public Transform joint0, joint1, joint2;
	Vector3 baseFrame, link1;
	float theta, lastTheta;
	
	public Slider sliderTheta2;
	
	void Start()
	{
		//get x vector of base
		baseFrame = joint0.forward;
		
		//get vector from joint 0 to 1
		link1 = joint1.position - joint0.position;
		
		//get the angle (in degrees) between the vectors along the links
		theta = Vector3.Angle(baseFrame, link1);
		
		//give angle correct sign
		if(!(RobotBase.rotation.eulerAngles.y == 0))
		{
			if((AngleDir(baseFrame, link1,Quaternion.Euler(-90, 0, 0) * baseFrame) < 0f) && (RobotBase.rotation.eulerAngles.y > 180))
			{
				theta*=-1;
			}
			else if((AngleDir(baseFrame, link1,Quaternion.Euler(-90, 0, 0) * baseFrame) >= 0f) && (RobotBase.rotation.eulerAngles.y < 180))
			{
				theta*=-1;
			}
			
		}
		//handles special case for base being at 0
		else
		{
			if((AngleDir(baseFrame, link1,Quaternion.Euler(-180, 0, 0) * baseFrame) == -1f))
			{
				theta*=-1;
			}
		}

		//set slider values
		lastTheta = theta;
		sliderTheta2.value = theta;
		
		
	}

	//handles slider during automation
	void Update () 
	{
		if(DHParameters.getMoveSlider())
		{
			sliderTheta2.value = DHParameters.getTheta2();
			lastTheta = DHParameters.getTheta2();
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
	
	//called when theta3 slider is moved.  rotates lower arm
	public void SliderJoint1(float angle)
	{
		if(DHParameters.getMoveSlider() == false)
		{
			RobotLowerArm.Rotate (-(angle - lastTheta), 0f , 0f);
			lastTheta = angle;
		}
		
	}
	
	
}
