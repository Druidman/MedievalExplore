using Godot;
using System;

public partial class SfxPlayer : AudioStreamPlayer3D
{
	// Called when the node enters the scene tree for the first time.

  public override void _PhysicsProcess(double delta)
  {
	this.VolumeDb = 10 / GameGlobals.soundEffectsVolume;
  }

	
}
