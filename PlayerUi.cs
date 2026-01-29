using Godot;
using System;

public partial class PlayerUi : Control
{

	[Export]
	Player player;

	[Export]
	Label coins;
	public override void _Process(double delta)
	{
		coins.Text = this.player.coins.ToString();
	}
}
