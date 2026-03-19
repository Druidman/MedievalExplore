using Godot;
using System;

public enum CharacterType
{
	Mage,
	Knight,
	Barbarian
}
public partial class CharacterModel : Node3D
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	protected NodePath magePath = "Mage";

	[Export]
	protected NodePath knightPath = "Knight";

	[Export]
	protected NodePath barbarianPath = "Barbarian";

  protected Node3D mage;
  protected Node3D knight;
  protected Node3D barbarian;


	public CharacterType characterType = CharacterType.Knight;
	public void ChangeCharacter(CharacterType charType)
	{
		this.characterType = charType;
		showCurrentCharacter();
	}
	protected void HideCharacter(CharacterType charType)
	{
		switch (charType)
		{
			case CharacterType.Mage:
				mage.Visible = false;
				mage.ProcessMode = ProcessModeEnum.Disabled;
				mage.PhysicsInterpolationMode = PhysicsInterpolationModeEnum.Off;

				break;
			case CharacterType.Knight:
				knight.Visible = false;
				knight.ProcessMode = ProcessModeEnum.Disabled;
				knight.PhysicsInterpolationMode = PhysicsInterpolationModeEnum.Off;
				break;		
			case CharacterType.Barbarian:
				barbarian.Visible = false;
				barbarian.ProcessMode = ProcessModeEnum.Disabled;
				barbarian.PhysicsInterpolationMode = PhysicsInterpolationModeEnum.Off;
				break;
			default:
				break;

		}
	}

	protected void ShowCharacter(CharacterType charType)
	{
		switch (charType)
		{
			case CharacterType.Mage:
				mage.Visible = true;
				mage.ProcessMode = ProcessModeEnum.Inherit;
				mage.PhysicsInterpolationMode = PhysicsInterpolationModeEnum.On;

				break;
			case CharacterType.Knight:
				knight.Visible = true;
				knight.ProcessMode = ProcessModeEnum.Inherit;
				knight.PhysicsInterpolationMode = PhysicsInterpolationModeEnum.On;
				break;		
			case CharacterType.Barbarian:
				barbarian.Visible = true;
				barbarian.ProcessMode = ProcessModeEnum.Inherit;
				barbarian.PhysicsInterpolationMode = PhysicsInterpolationModeEnum.On;
				break;
			default:
				break;

		}
	}
	protected void showCurrentCharacter()
	{
		// hide all first
		HideCharacter(CharacterType.Mage);
		HideCharacter(CharacterType.Knight);
		HideCharacter(CharacterType.Barbarian);
		// show current one
		ShowCharacter(characterType);
	}
	public override void _Ready()
  {
	mage = GetNode<Node3D>(magePath);
	knight = GetNode<Node3D>(knightPath);
	barbarian = GetNode<Node3D>(barbarianPath);
		showCurrentCharacter();

	}

	public void NextCharacter(){
		switch (characterType)
		{
			case CharacterType.Mage:
				ChangeCharacter(CharacterType.Knight);
				break;
			case CharacterType.Knight:
				ChangeCharacter(CharacterType.Barbarian);
				break;		
			case CharacterType.Barbarian:
				ChangeCharacter(CharacterType.Mage);
				break;
			default:
				break;

		}
	}

  

	
}
