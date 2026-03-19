using Godot;
using System;

public partial class Player : CharacterBody3D
{
	private const float SPEED = 1.0f;
	private const float DECELERATION_SPEED = SPEED * 0.1f;

	private const float JUMP_FORCE = 1f;
	private const float GRAVITY_SPEED = 3f;

	Godot.Vector3 velocity;
	[Export]
	public Camera3D camera;

	[Export]
	CharacterModel characterModel;

	[Export]
	PackedScene treeScene;
	private float upward_force = 0f;

	[Export]
	AudioStreamPlayer3D walkingPlayer;


	public bool eventsBlocked = false;

	public int coins {get; protected set; } = 10;

	public int seeds = 0;
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
		characterModel.ChangeCharacter(GameGlobals.characterPicked);
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

		if (Input.IsActionPressed("dashSpeed"))
		{
			moveVec *= SPEED * 2;
		}
		else
		{
			moveVec *= SPEED;	
		}

		

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
			if (Input.IsActionJustPressed("placeTree") && seeds >= 1 && IsOnFloor())
			{
				Tree tree = treeScene.Instantiate<Tree>();
				tree.Initialize(this.GlobalPosition + (new Godot.Vector3(1,0,0)).Rotated(Godot.Vector3.Up, this.Rotation.Y + Mathf.DegToRad(90)));
				GetParent().AddChild(tree);
				seeds -= 1;
			}

			



			this.HandleMovement();
		}

		velocity.Z = Mathf.MoveToward(velocity.Z, 0, DECELERATION_SPEED);
		velocity.X = Mathf.MoveToward(velocity.X, 0, DECELERATION_SPEED);
		

		

		
		Velocity = velocity;
		MoveAndSlide();

		if (this.GlobalPosition.Y < -5)
		{
			this.GlobalPosition = new Godot.Vector3(0,5,0);
		}

		if (this.Velocity.X > 0 || this.Velocity.Z > 0)
		{
			if (this.walkingPlayer.Playing) return;
			this.walkingPlayer.Play();
		}
		else
		{
			if (!this.walkingPlayer.Playing) return;
			this.walkingPlayer.Stop();
		}
	}
}
