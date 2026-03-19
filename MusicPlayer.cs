public partial class MusicPlayer : Godot.AudioStreamPlayer3D
{
  public override void _Ready()
	{
	this.Bus = "MUSIC";
  }
}
