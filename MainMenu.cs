using Godot;
using System;


public partial class MainMenu : Control
{
	[Export]
	Control settings;
	public void OnExit()
	{
		GetTree().Quit();
	}
	public void OnPlay()
	{
		GetTree().ChangeSceneToFile("res://game.tscn");
	}

  public override void _Ready()
  {
	Input.MouseMode = Input.MouseModeEnum.Visible;
  }
	public void OnSettings()
	{
		settings.Visible = true;
		this.Visible = false;
	}

}
