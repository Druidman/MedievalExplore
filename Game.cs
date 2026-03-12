using Godot;
using System;


public partial class Game : Node3D
{
	const double zAxisDelta = 1.732; // just tested
	const double xAxisDelta = 1; // just tested
	
	const double tileWidth = 2;
	const double tileHeight = 3.464;

	static int worldTilesX = 5; // x
	static int worldTilesZ = 5; // z

	[Export]
	PackedScene tileScene = null;

	[Export]
	PackedScene treeSeedScene = null;
	
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
		GenerateBaseTiles();

		for (int i = 0; i< 5; i++) SpawnSeed(); // spawn 5 first seeds
	}


	public void SpawnSeed()
	{
		float x = Random.Shared.Next((int)(-worldTilesX / 2 * tileWidth), (int)(worldTilesX / 2 * tileWidth));
		float z = Random.Shared.Next((int)(-worldTilesZ / 2 * tileHeight), (int)(worldTilesZ / 2* tileHeight));

		TreeSeed seed = treeSeedScene.Instantiate<TreeSeed>();
		seed.Initialize(new Godot.Vector3(x, 0, z));

		AddChild(seed);
	}
	
	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent.IsActionPressed("return"))
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
			GetTree().ChangeSceneToFile("res://gameMenu.tscn");
		}
		
	}

	public void GenerateBaseTiles()
	{
		if (this.tileScene is null) return;


		bool shouldApplyOffset = false;

		for (double zPos = (-worldTilesZ / 2) * tileHeight; zPos <= (worldTilesZ / 2) * tileHeight; zPos += zAxisDelta)
		{
			for (double xPos = (-worldTilesX / 2) * tileWidth; xPos <= (worldTilesX / 2) * tileWidth; xPos += tileWidth)
			{
				Godot.Vector3 tilePos = new Godot.Vector3((float)xPos,0,(float)zPos);
				if (shouldApplyOffset) {
					tilePos += new Godot.Vector3((float)xAxisDelta,0,0);
				}
				Tile tile = tileScene.Instantiate<Tile>();
				tile.Initialize(tilePos);
				AddChild(tile);
			}	
			shouldApplyOffset = !shouldApplyOffset;
		}
	}
}
