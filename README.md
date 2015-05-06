# Unity3D-Robotic-Arm-Simulator
A very basic control system for a simulated robotic arm using Unity 5.


CURRENT FUNCTIONS:

Direct Control Using Sliders: Each theta value for the robotic arm is represented by a slider that the users can directly control. This is useful for manually putting the robot in a desired position or to see how different theta values will affect the robot’s configuration.

Knot Points: After manually configuring the robot, users can save the configuration as a knot point. Up to five points can be saved at a time simply by clicking a button when the robot is in the desired configuration. After entering a time duration, the user can then have the robotic arm cycle through each of the saved knot points, each point taking the specified time duration.

Stop Command: Users can click the stop button at any point during automated tasks (such as running through knot points) in order to make the robot come to an immediate halt at its current position.

Go To Position (Currently In Progress): Users will specify an x, y, z coordinate in the simulator’s 3D space along with a time duration and the robot will configure such that it’s end effector is positioned at the specified point.
