using Godot;
using System;

public partial class Tile : MeshInstance3D
{

	private Godot.Vector3 globalPos;

	public void Initialize(Godot.Vector3 globalPos)
	{
		this.globalPos = globalPos;
	}

	public override void _Ready()
	{
		this.GlobalPosition = globalPos;
		
	}
}
