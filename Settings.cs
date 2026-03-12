using Godot;
using System;

public partial class Settings : Control
{
	[Export]
	Slider musicVolume;

	[Export]
	Slider soundEffectsVolume;

	[Signal]
  public delegate void OnSettingsReturnEventHandler();

	public void onReturn()
	{
		EmitSignal(SignalName.OnSettingsReturn);
	}

  public override void _Ready()
  {
	musicVolume.Value = GameGlobals.musicVolume;
		soundEffectsVolume.Value = GameGlobals.soundEffectsVolume;
  }

  public void onMusicSliderChange(float value)
	{
		GameGlobals.musicVolume = value;
	}
	public void onSoundEffectsChange(float value)
	{
		GameGlobals.soundEffectsVolume = value;
	}
	
}
