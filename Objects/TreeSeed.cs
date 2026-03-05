using Godot;

public partial class TreeSeed : Node3D
{
  Godot.Vector3 globalPos = default(Godot.Vector3);


  public void Initialize(Godot.Vector3 globalPos)
  {
	this.globalPos = globalPos;
  }

  public override void _Ready()
  {
	this.GlobalPosition = this.globalPos;
  }
  public void OnBodyExited(Node3D body)
  {
	
  }

  public void OnBodyEntered(Node3D body)
  {
	if (body is Player player)
	{
	  player.seeds += 1;
	GetParent().RemoveChild(this);
	QueueFree();
	}
  }
}
