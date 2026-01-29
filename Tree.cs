using Godot;

public partial class Tree : StaticBody3D
{
	[Export]
	MeshInstance3D guideText;

	[Export]
	MeshInstance3D destroyer;

	[Export]
	Timer destroyerTimer;
	bool isFullyGrown = true;
	Player player = null;
	public void OnBodyEntered(Node3D body)
	{
		if (!isFullyGrown) return;

		if (body is not Player player) return;

		this.guideText.Visible = true;
		this.guideText.GlobalPosition = player.GlobalPosition + new Godot.Vector3(0,5,0);
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
		if (inputEvent.IsActionPressed("DestroyTree") && player != null)
		{
			this.StartDestroyer();
		}
	}

	public void StartDestroyer()
	{
		this.destroyer.Visible = true;
		
		this.destroyerTimer.Start();
	}
	public override void _Process(double delta)
	{
		if (this.destroyer.Visible)
		{
			this.destroyer.Position = this.destroyer.Position.Rotated(Godot.Vector3.Up, Mathf.DegToRad(360*(float)delta));
		}
	}

	public void OnDestroyEnd()
	{
		

		this.destroyer.Visible = false;
		
		this.destroyerTimer.Stop();

		if (this.player == null) return;
		this.player.AddCoins(20);
		
		GetParent().GetParent().RemoveChild(GetParent());

		GetParent().QueueFree();

		
	}
}
