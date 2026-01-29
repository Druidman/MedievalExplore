using Godot;
using System;

public partial class Camera : Camera3D
{
	private const int movementRadius = 15;
	private Vector3 cameraOffset = new Vector3(0, 10, 15);
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
		LookAtFromPosition(Position, player.GlobalPosition);
		UpdateCamera();
	}

	public override void _Input(InputEvent @event)
	{
		if (this.player.eventsBlocked) return;
		
		if (@event is InputEventMouseMotion motion)
		{
			angle -= motion.Relative.X * 0.01f;
		}
	}

	public override void _Process(double delta)
	{
		UpdateCamera();
	}
}
