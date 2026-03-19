using Godot;
using System;
using System.Collections.Generic;

public partial class GameMenuScreen : Node3D
{

	[Export]
	Settings settings;

	[Export]
	PackedScene gameScene;

	[Export]
	CharacterViewer characterViewer;
  // Called when the node enters the scene tree for the first time.

	World3DButton lastHoveredButton = null;

	[Export]
	AudioStreamPlayer3D audioPlayer;

  public override void _Input(InputEvent inputEvent)
  {
	if (settings.Visible) return;
	if (inputEvent.IsActionPressed("spaceClick"))
		{
			audioPlayer.Stop(); 
			audioPlayer.Play();
			GetTree().ChangeSceneToPacked(gameScene);
			GameGlobals.characterPicked = characterViewer.characterType;
		}
	if (inputEvent.IsActionPressed("mouseClick"))
		{
			Area3D area = this.GetAreaUnderMouse();

			if (area is SettingsButton areaB)
			{
				audioPlayer.Stop();
				audioPlayer.Play();
				settings.Visible = true;
			}
			if (area is ExitButton areaE)
			{
				audioPlayer.Stop();
				audioPlayer.Play();
				GetTree().Quit();
			}
			if (area is NextButton nButton)
			{
				audioPlayer.Stop();
				audioPlayer.Play();
				characterViewer.NextCharacter();

			}
		}
  }
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (settings.Visible) return;
		Area3D area = this.GetAreaUnderMouse();

		if (area is World3DButton areaB)
		{
	
			areaB.MakeMeshGreyish();
			lastHoveredButton = areaB;
		}
		else
		{
			if (lastHoveredButton != null)
			{
				lastHoveredButton.unhovered();
				lastHoveredButton = null;	
			}
			
		}

		
	}

	public void onSettingsReturn()
	{
		settings.Visible = false;
	}

	public Area3D GetAreaUnderMouse(float rayLength = 1000.0f)
	{
		var viewport = GetViewport();
		var mousePos = viewport.GetMousePosition();
		var camera = viewport.GetCamera3D();

		if (camera == null) return null;

		// 1. Project the mouse position into 3D space
		Vector3 origin = camera.ProjectRayOrigin(mousePos);
		Vector3 normal = camera.ProjectRayNormal(mousePos);
		Vector3 target = origin + (normal * rayLength);

		// 2. Access the Physics World State
		var spaceState = GetWorld3D().DirectSpaceState;
		
		// 3. Create the query
		var query = PhysicsRayQueryParameters3D.Create(origin, target);
		
		// Optional: Enable Area detection (disabled by default)
		query.CollideWithAreas = true;
		query.CollideWithBodies = false;

		// 4. Cast the ray
		Godot.Collections.Dictionary result = spaceState.IntersectRay(query);

		// 5. Check if we hit something and return the Area3D
		if (result.Count > 0)
		{
			return result["collider"].As<Area3D>();
		}

		return null;
	}
}
