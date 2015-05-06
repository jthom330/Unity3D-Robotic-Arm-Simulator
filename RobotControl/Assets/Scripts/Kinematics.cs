using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Kinematics : MonoBehaviour {

	public Transform baseFrame;
	public Transform lowerArm;
	public Transform upperArm;
	public Transform baseReference;

	public InputField inputX;
	public InputField inputY;
	public InputField inputZ;
	public InputField time;

	private Vector3 target;

	//Called when the go button is pressed 
	public void StartKinematics()
	{
		if(time.text != null && inputX.text != null && inputY.text != null && inputZ.text != null)
		{
			target = new Vector3(float.Parse(inputX.text), float.Parse(inputY.text), float.Parse(inputZ.text));
			StartCoroutine(RotateBaseToTarget(float.Parse(time.text)));
			StartCoroutine(ConfigureArm(float.Parse(time.text)));


		}
	}

	//rotates robot base to face desired point
	IEnumerator RotateBaseToTarget(float inTime)
	{

		//values for internal use
		Quaternion _lookRotation;
		Vector3 _direction;
		
		//find the vector pointing from our position to the target
		_direction = (target - baseFrame.position);
		
		//create the rotation we need to be in to look at the target
		_lookRotation = Quaternion.LookRotation(_direction);
		Quaternion start = baseFrame.rotation;

		DHParameters.setMoveSlider (true);
		for(float t = 0f ; t < 1f ; t += Time.deltaTime/inTime)
		{
			//rotate us over time according to speed until we are in the required rotation
			baseFrame.localRotation = Quaternion.Euler(0f, (Quaternion.Lerp(start, _lookRotation, t)).eulerAngles.y, 0f);
			yield return null ;
		}
		DHParameters.setMoveSlider (false);


	}

	IEnumerator ConfigureArm(float inTime)
	{
		//get vector from base to target
		Vector3 toTarget = target - baseReference.position;
		//get similar vector, but ignoring y
		Vector3 bottom = new Vector3(target.x, baseReference.position.y, target.z) - baseReference.position;

		//use angle between vectors to get phi
		float phi = Vector3.Angle(toTarget, bottom);

		//get x, y of target
		//we are treating arm links as if they are planar.  base hadled in other thread
		float Y = toTarget.y;
		float X = Vector3.Distance(new Vector3(baseReference.position.x, target.y, baseReference.position.z), target);

		//equation for cos and sin
		float c2 = (Mathf.Pow (X, 2f) + Mathf.Pow (Y, 2f) - Mathf.Pow (DHParameters.getDist1to2(), 2f) - Mathf.Pow (DHParameters.getDist2to3(), 2f)) / (2f * DHParameters.getDist1to2() * DHParameters.getDist2to3());
		float s2 = Mathf.Sqrt(1 - Mathf.Pow(c2,2f));

		//get thetas, if possible
		float theta1 = Mathf.Atan2 (s2, c2);

		if(theta1 > 90 || theta1 < 0)
		{
			theta1 = Mathf.Atan2 (-s2, c2);
		}

		float theta2 = phi - theta1;

		//stop execution if thetas cannot be found
		if(float.IsNaN(theta1) || float.IsNaN(theta2))
		{
			DHParameters.setMoveSlider (false);
			GetComponent<Kinematics>().StopAllCoroutines();
				
		}

		//calculate start and end values of desired rotations
		Quaternion la = lowerArm.localRotation;
		Quaternion ua = upperArm.localRotation;
		Quaternion laTo = Quaternion.Euler (-theta1 + DHParameters.getTheta2 ()+la.eulerAngles.x, la.eulerAngles.y, la.eulerAngles.z);
		Quaternion uaTo;
		if(theta2 >= DHParameters.getTheta3 ())
		{
			uaTo = Quaternion.Euler (-theta2 + DHParameters.getTheta3 ()+ua.eulerAngles.x, ua.eulerAngles.y, ua.eulerAngles.z);
		}
		else
		{
			uaTo = Quaternion.Euler (theta2 - DHParameters.getTheta3 ()+ua.eulerAngles.x, ua.eulerAngles.y, ua.eulerAngles.z);
		}

		//rotate links to desired positions over time 
		DHParameters.setMoveSlider (true);
		for(float t = 0f ; t < 1f ; t += Time.deltaTime/inTime)
		{
			lowerArm.localRotation = Quaternion.Lerp(la, laTo, t);
			upperArm.localRotation = Quaternion.Lerp(ua, uaTo, t);
			
			yield return null ;
		}
		DHParameters.setMoveSlider (false);

		yield return null ;
	}

}
