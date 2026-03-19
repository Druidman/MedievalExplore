using Godot;
using System;
using System.Linq;

public partial class SlotMachine : StaticBody3D
{
	Random slotRandomNumGenerator = new Random();
	[Export]
	MeshInstance3D instructionText;

	[Export]
	SfxPlayer rollingPlayer;

	[Export]
	Control ui;

	[Export]
	Player player;

	[Export]
	Timer rollTimer;

	bool rolling = false;


	[Export]
	Godot.Collections.Array<Label> slots;
	

	bool playerIn = false;

	public void closeUi()
	{

		if (this.rolling) return;

		this.ui.Visible = false;

		this.player.UnblockEvents();
				
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}
	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent.IsActionPressed("ToggleSlotMachineUi") && playerIn)
		{
			if (this.rolling) return;
			if (this.ui.Visible)
			{
				
				this.player.UnblockEvents();
				
				Input.MouseMode = Input.MouseModeEnum.Captured;
				
			}
			else
			{
				this.player.BlockEvents();	
				Input.MouseMode = Input.MouseModeEnum.Visible;
			}
			this.ui.Visible = !this.ui.Visible;

		}
	}

	public void OnBodyEntered(Godot.Node3D body)
	{
		if (body is not Player) return;

		instructionText.Visible = true;
		instructionText.Rotation = body.Rotation;	
		this.playerIn = true;	
	}

	public void OnBodyExited(Godot.Node3D body)
	{
		if (body is not Player) return;

		instructionText.Visible = false;
		this.playerIn = false;


		this.ui.Visible = false;
		this.player.UnblockEvents();
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public override void _Process(double delta)
	{
		if (this.rolling)
		{
			foreach (Label Slot in slots)
			{
				
				Slot.Text = this.slotRandomNumGenerator.Next(0,10).ToString();
			}
		}
	}


	public void OnRollPressed()
	{
		if (this.player.coins < 5) // magic num TODO
		{
			return;
		}

		if (this.rolling) return;

		

		this.StartRoll();
	}

	private void StartRoll()
	{

		this.rollTimer.Stop(); // just in case

		this.rollingPlayer.Stop();
		this.rollingPlayer.Play();

		this.rollTimer.Start();
		this.rolling = true;
		this.player.TakeCoins(5);
	}

	public void OnRollEnd()
	{
		this.rolling = false;
		this.rollTimer.Stop(); // in case
		this.rollingPlayer.Stop();
		
		//checking result

		if (slots.Count <= 0) return;

		int winNum = slots.ElementAtOrDefault(0).Text.ToInt();

		foreach (Label Slot in slots)
		{
			if (Slot.Text.ToInt() != winNum)
			{
				return; // no win
			}
		}

		// win
		this.player.AddCoins(100);
	}
}
