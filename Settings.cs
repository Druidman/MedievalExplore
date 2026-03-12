using Godot;
using System;

public partial class Settings : Control
{

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
		

		_busIndex = AudioServer.GetBusIndex(BusName);

		// Initialize the slider to the current volume
		float currentDb = AudioServer.GetBusVolumeDb(_busIndex);
		soundEffectsVolume.Value = Mathf.DbToLinear(currentDb);
  }

	public void onSoundEffectsChange(float value)
	{

		GD.Print("siema");
		// Convert the 0.0 - 1.0 slider value to Decibels (dB)
		// Mathf.LinearToDb handles the logarithmic conversion for you
		float dbValue = (float)Mathf.LinearToDb(value);
		
		AudioServer.SetBusVolumeDb(_busIndex, dbValue);

		// Mute the bus entirely if the slider is at 0
		AudioServer.SetBusMute(_busIndex, value <= 0);
	}


	// The name of the Bus in the Audio tab (usually "Master")
	[Export] public string BusName = "Master";
	private int _busIndex;

	
}
