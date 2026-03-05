using Godot;
using System;

public partial class Tree : Node3D
{

  Godot.Vector3 globalPos = default(Godot.Vector3);
	[Export]
	MeshInstance3D guideText;

	[Export]
	MeshInstance3D destroyer;

  [Export]
	Node3D treeModel;

	[Export]
	Timer destroyerTimer;

  [Export]
  Timer growTimer;

	[Export]
	AudioStreamPlayer3D StreamPlayer;
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
			this.StartDestroyer();
		}
	}

	public void StartDestroyer()
	{
		this.destroyer.Visible = true;
		
		this.destroyerTimer.Start();
		this.StreamPlayer.Play();
	}
	public override void _Process(double delta)
	{
	
		if (this.destroyer.Visible)
		{
			this.destroyer.Position = this.destroyer.Position.Rotated(Godot.Vector3.Up, Mathf.DegToRad(360*(float)delta));
		}

	if (!isFullyGrown)
	{
	  float scale = (float)(30 - this.growTimer.TimeLeft) / 30; // 30 for 30s
	if (scale < 0.2) scale = 0.2f;
	  treeModel.Scale = new Vector3(scale, scale, scale);

	}


	}

	public void OnDestroyEnd()
	{
		
		this.StreamPlayer.Stop();
		this.destroyer.Visible = false;
		
		this.destroyerTimer.Stop();

		if (this.player == null) return;
		this.player.AddCoins(20);
		
		GetParent().RemoveChild(this);

		QueueFree();

		
	}

  public void OnGrown()
  {
	this.isFullyGrown = true;
  }
}
