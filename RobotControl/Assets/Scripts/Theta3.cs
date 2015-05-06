using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Theta3 : MonoBehaviour {

	public Transform joint0, joint1, joint2;
	Vector3 link1, link2;
	float theta;
	public Transform baseRotation;
	public Text theta3Text;
	
	// Update is called once per frame
	void Update () 
	{
		
		//get vector from joint 0 to 1
		link1 = joint1.position - joint0.position;
		
		//get vector from joint 1 to 2
		link2 = joint2.position - joint1.position;
		
		//get the angle (in degrees) between the vectors along the links
		theta = Vector3.Angle(link1, link2);

		//give angle correct sign
		if((AngleDir(link1, link2,Quaternion.Euler(0, -90, 0) * link1) < 0f) || DHParameters.getTheta2() >= 90f)//&& (baseRotation.rotation.eulerAngles.y > 180))
			{
				theta*=-1;
			}
			
		//sets and displays theta
		DHParameters.setTheta3(theta);
		theta3Text.text = ""+theta;

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
	
	
}
