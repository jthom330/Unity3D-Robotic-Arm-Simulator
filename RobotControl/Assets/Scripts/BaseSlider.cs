using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BaseSlider : MonoBehaviour {

	float theta;
	public Transform RobotBase;
	public Slider sliderTheta1;

	// sets robot base slider to the appropriate starting position 
	void Start () {
		theta = RobotBase.rotation.y;
		sliderTheta1.value = theta;
	
	}
	
	//during an automated process, update the slider
	void Update () 
	{
		if(DHParameters.getMoveSlider())
		{
			sliderTheta1.value = DHParameters.getTheta1();
		}
	}

	//called when theta1 slider is moved.  rotates base
	public void SliderJoint0(float angle)
	{
		if(DHParameters.getMoveSlider() == false)
		{
			RobotBase.rotation =  Quaternion.Euler(0f, -angle , 0f);

		}
		
	}
}
