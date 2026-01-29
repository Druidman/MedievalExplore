using Godot;
using System;

public partial class Player : CharacterBody3D
{
	private const float SPEED = 10.0f;
	private const float DECELERATION_SPEED = SPEED * 0.1f;

	private const float JUMP_FORCE = 10f;
	private const float GRAVITY_SPEED = 20f;

	Godot.Vector3 velocity;
	[Export]
	public Camera3D camera;
	private float upward_force = 0f;


	public bool eventsBlocked = false;

	public int coins {get; protected set; } = 5;


	public void AddCoins(int amountToAdd)
	{
		if (amountToAdd < 0) return;

		this.coins += amountToAdd;
	}

	public void TakeCoins(int amountToRemove)
	{
		if (amountToRemove < 0) return;

		this.coins -= amountToRemove;
	}

	public override void _Ready()
	{
		GD.Print("player ready");
	}

	public void BlockEvents()
	{
		this.eventsBlocked = true;
	}

	public void UnblockEvents()
	{
		this.eventsBlocked = false;
	}
	

	public void HandleMovement()
	{
		Vector3 moveVec = Vector3.Zero;

		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JUMP_FORCE;
		}

		if (Input.IsActionPressed("go_front"))
			moveVec.Z += -1;
		if (Input.IsActionPressed("go_back"))
			moveVec.Z += 1;
		if (Input.IsActionPressed("go_left"))
			moveVec.X += -1;
		if (Input.IsActionPressed("go_right"))
			moveVec.X += 1;

		moveVec *= SPEED;

		moveVec = moveVec.Rotated(Vector3.Up, Rotation.Y);

		if (moveVec.Z != 0)
		{
			velocity.Z = moveVec.Z;
		}
		else
		{
			velocity.Z = Mathf.MoveToward(velocity.Z, 0, DECELERATION_SPEED);
		}

		if (moveVec.X != 0)
		{
			velocity.X = moveVec.X;
		}
		else
		{
			velocity.X = Mathf.MoveToward(velocity.X, 0, DECELERATION_SPEED);
		}
	}
	public override void _PhysicsProcess(double delta)
	{
		
		if (IsOnFloor())
		{
			velocity.Y = 0;
		}
		else
		{
			velocity.Y -= GRAVITY_SPEED * (float)delta;
		}

		if (!this.eventsBlocked)
		{
			if (!Input.IsActionPressed("freeCamera"))
			{
				Rotation = new Vector3(Rotation.X, camera.GlobalRotation.Y, Rotation.Z);
			}



			this.HandleMovement();
		}

		velocity.Z = Mathf.MoveToward(velocity.Z, 0, DECELERATION_SPEED);
		velocity.X = Mathf.MoveToward(velocity.X, 0, DECELERATION_SPEED);
		

		

		
		Velocity = velocity;
		MoveAndSlide();
	}
}
