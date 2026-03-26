using Godot;
using System;

public partial class Camera : Camera3D
{
	
	private Vector3 cameraOffset = new Vector3(0, 2.5f, 2.25f);
	private float angle = 0.0f; 

	[Export]
	Player player;

	private void UpdateCamera()
	{
		var pivot = player.GlobalPosition;
		var offset = cameraOffset;

		offset = offset.Rotated(Vector3.Up, angle);

		GlobalPosition = pivot + offset;
		GlobalRotation = new Vector3(GlobalRotation.X, angle, GlobalRotation.Z);
	}

	public override void _Ready()
	{
		GlobalPosition = player.GlobalPosition + cameraOffset;
		LookAtFromPosition(GlobalPosition, player.GlobalPosition);
		UpdateCamera();
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("RotateCameraLeft"))
		{
			angle += Mathf.DegToRad(180) * (float)delta;
			
		}
		if (Input.IsActionPressed("RotateCameraRight"))
		{
			angle -= Mathf.DegToRad(180) * (float)delta;

		}
		UpdateCamera();
		
	}
}
