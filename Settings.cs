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
		float linearValue = Mathf.DbToLinear(currentDb);
		// Convert back to 0-100 range to match slider
		soundEffectsVolume.Value = linearValue * 100f;
  }

	public void onSoundEffectsChange(float value)
	{

		GD.Print("siema");
		// Normalize slider value (0-100) to 0.0-1.0 range
		float normalizedValue = value / 100f;
		
		// Convert the 0.0 - 1.0 slider value to Decibels (dB)
		// Mathf.LinearToDb handles the logarithmic conversion for you
		float dbValue = (float)Mathf.LinearToDb(normalizedValue);
		
		AudioServer.SetBusVolumeDb(_busIndex, dbValue);

		// Mute the bus entirely if the slider is at 0
		AudioServer.SetBusMute(_busIndex, value <= 0);
	}


	// The name of the Bus in the Audio tab (usually "Master")
	[Export] public string BusName = "Master";
	private int _busIndex;

	
}
