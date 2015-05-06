using UnityEngine;
using System.Collections;

public class EmergencyStop : MonoBehaviour {

	public GameObject scriptHolder;

	//called when the stop button is pressed
	//halts all automated processes
	public void Stop()
	{
		GetComponent<KnotPoints>().StopAllCoroutines();
		GetComponent<Kinematics>().StopAllCoroutines();
		//Add any automation script

		DHParameters.setMoveSlider (false);

	}

}
