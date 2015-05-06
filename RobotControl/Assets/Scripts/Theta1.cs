using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Theta1 : MonoBehaviour {

	float theta;
	public Transform RobotBase;
	public Text theta1Text;
	private float x;

	
	//sets and displays theta1 each frame
	void Update () {
	
		theta = RobotBase.eulerAngles.y;

		//sets and displays theta
		if(theta == 0f)
		{
			x = 0f;
		}
		else
		{
			x = 360f;
		}

		DHParameters.setTheta1(x-theta);
		theta1Text.text = ""+(x-theta);
	}
}
