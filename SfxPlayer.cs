using Godot;

public partial class SfxPlayer : AudioStreamPlayer3D
{
	public override void _Ready()
	{
	this.Bus = "SFX";
  }

	
}
