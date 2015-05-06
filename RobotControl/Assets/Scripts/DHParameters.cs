
//static class for holding parameters for the robot

public class DHParameters
{
	private static float theta1, theta2, theta3;
	private static float dist0to1, dist1to2, dist2to3;
	private static float twist0to1 = 0f, twist1to2 = 90f, twist2to3 = 0f;
	private static bool moveSliders = false;

	//getters and setters for slider boolean
	//determines if sliders move automatically
	public static bool getMoveSlider()
	{
		return moveSliders;
	}

	public static void setMoveSlider(bool b)
	{
		moveSliders = b;
	}

	//getters and setters for thetas
	public static float getTheta1()
	{
		return theta1;
	}

	public static void setTheta1(float x)
	{
		theta1 = x;
	}

	public static float getTheta2()
	{
		return theta2;
	}
	
	public static void setTheta2(float x)
	{
		theta2 = x;
	}

	public static float getTheta3()
	{
		return theta3;
	}
	
	public static void setTheta3(float x)
	{
		theta3 = x;
	}


	//getters and setters for distances (a)
	public static float getDist0to1()
	{
		return dist0to1;
	}
	
	public static void setDist0to1(float x)
	{
		dist0to1 = x;
	}
	
	public static float getDist1to2()
	{
		return dist1to2;
	}
	
	public static void setDist1to2(float x)
	{
		dist1to2 = x;
	}
	
	public static float getDist2to3()
	{
		return dist2to3;
	}
	
	public static void setDist2to3(float x)
	{
		dist2to3 = x;
	}

	//getters for twist parameters
	public static float getTwist0to1()
	{
		return twist0to1;
	}

	public static float getTwist1to2()
	{
		return twist1to2;
	}

	public static float getTwist2to3()
	{
		return twist2to3;
	}


}
