using Godot;
using System;

public partial class Tree : Node3D
{

  Godot.Vector3 globalPos = default(Godot.Vector3);
	[Export]
	MeshInstance3D guideText;


	float hp = 5;


  [Export]
	Node3D treeModel;


  [Export]
  Timer growTimer;

	[Export]
	SfxPlayer treeDestroyPlayer;
	bool isFullyGrown = false;
	Player player = null;

  public void Initialize(Godot.Vector3 globalPos)
  {
	this.globalPos = globalPos;
  }

  public override void _Ready()
  {
	this.GlobalPosition = globalPos;
  }
	public void OnBodyEntered(Node3D body)
	{
		if (!isFullyGrown) return;

		if (body is not Player player) return;

		this.guideText.Visible = true;
		this.guideText.Rotation = player.Rotation;

		this.player = player;

	}

	public void OnBodyExited(Node3D body)
	{
		if (!isFullyGrown) return;

		if (body is not Player player) return;

		this.guideText.Visible = false;
		this.player = null;
	}

	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent.IsActionPressed("DestroyTree") && player != null && isFullyGrown)
		{
			if (this.player.isDestroyingTree)
			{
				this.player.StopTreeDestroy();
			}
			else
			{
				this.player.StartTreeDestroy(this);	
			}
			
		}
	}
	public override void _Process(double delta)
	{
	
		

		if (!isFullyGrown)
		{
			float scale = (float)(30 - this.growTimer.TimeLeft) / 30; // 30 for 30s
			if (scale < 0.2) scale = 0.2f;
			treeModel.Scale = new Vector3(scale, scale, scale);

		}


	}

	public void OnDestroy()
	{

		if (this.player == null) return;
		this.player.AddCoins(20);
		this.player.StopTreeDestroy();
		
		GetParent().RemoveChild(this);

		QueueFree();

		
	}

  public void OnGrown()
  {
		this.isFullyGrown = true;
  }

	public void OnAxeEntered(Area3D area)
	{
		if (this.player == null) return;
		if (!this.player.isDestroyingTree) return;
		treeDestroyPlayer.Play();

		hp-=1;
		if (hp <= 0)
		{
			OnDestroy();
		}

	}
}
