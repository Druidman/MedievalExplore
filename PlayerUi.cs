using Godot;
using System;

public partial class PlayerUi : Control
{

	[Export]
	Player player;

	[Export]
	Label coins;
	[Export]
	Label seeds;
	public override void _Process(double delta)
	{
		coins.Text = this.player.coins.ToString();
		seeds.Text = this.player.seeds.ToString();
	}
}
