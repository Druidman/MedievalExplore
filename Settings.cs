using Godot;
using System;

public partial class Settings : Control
{

	[Export]
	Slider soundEffectsVolume;
	[Export]
	Slider musicEffectsVolume;

	[Signal]
  public delegate void OnSettingsReturnEventHandler();

	// The name of the Bus in the Audio tab (usually "Master")
	[Export] public string SFXBusName = "SFX";
	[Export] public string MusicBusName = "MUSIC";
	private int _SFXBusIndex;
	private int _MusicBusIndex;

	public void onReturn()
	{
		EmitSignal(SignalName.OnSettingsReturn);
	}
	private void AddBus(string name){
		int newBusIndex = AudioServer.BusCount;
		AudioServer.AddBus(newBusIndex);
		AudioServer.SetBusName(newBusIndex, name);
		
	}
  public override void _Ready()
  {
		AddBus(SFXBusName);
		AddBus(MusicBusName);

		_SFXBusIndex = AudioServer.GetBusIndex(SFXBusName);
		_MusicBusIndex = AudioServer.GetBusIndex(MusicBusName);


		soundEffectsVolume.Value = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(_SFXBusIndex)) * 100f;
		musicEffectsVolume.Value = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(_MusicBusIndex)) * 100f;
  }

	public void onSoundEffectsChange(float value)
	{
		// Normalize slider value (0-100) to 0.0-1.0 range
		float normalizedValue = value / 100f;
		
		// Convert the 0.0 - 1.0 slider value to Decibels (dB)
		// Mathf.LinearToDb handles the logarithmic conversion for you
		float dbValue = (float)Mathf.LinearToDb(normalizedValue);
		
		AudioServer.SetBusVolumeDb(_SFXBusIndex, dbValue);

		// Mute the bus entirely if the slider is at 0
		AudioServer.SetBusMute(_SFXBusIndex, value <= 0);
	}

	public void onMusicChange(float value)
	{
		// Normalize slider value (0-100) to 0.0-1.0 range
		float normalizedValue = value / 100f;
		
		// Convert the 0.0 - 1.0 slider value to Decibels (dB)
		// Mathf.LinearToDb handles the logarithmic conversion for you
		float dbValue = (float)Mathf.LinearToDb(normalizedValue);
		
		AudioServer.SetBusVolumeDb(_MusicBusIndex, dbValue);

		// Mute the bus entirely if the slider is at 0
		AudioServer.SetBusMute(_MusicBusIndex, value <= 0);
	}


	

	
}
