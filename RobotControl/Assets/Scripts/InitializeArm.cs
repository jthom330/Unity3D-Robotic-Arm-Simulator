using UnityEngine;
using System.Collections;

public class InitializeArm : MonoBehaviour {

	public Transform joint0, joint1, joint2;
	Vector3 link1, link2;
	float theta;
	public Transform baseRotation;

	//calculate the lengths of the links
	void Start () 
	{
		//set distance from referance frame to first frame
		DHParameters.setDist0to1 (0f);

		//get the distance (a) of link 1
		float dist = Vector3.Distance(joint0.position, joint1.position);
		DHParameters.setDist1to2 (dist);

		//get the distance (a) of link 2
		dist = Vector3.Distance(joint1.position, joint2.position);
		DHParameters.setDist2to3 (dist);


	}
	

}
