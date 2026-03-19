using Godot;
public partial class CharacterViewer : CharacterModel
{
	Godot.Vector3 characterRotation = new Godot.Vector3(0,0,0);

  public override void _Process(double delta)
  {
		characterRotation.Y += Mathf.DegToRad(60 * (float)delta);

		switch (characterType)
		{
			case CharacterType.Mage:
				mage.Rotation =characterRotation;
				break;
			case CharacterType.Knight:
				knight.Rotation =characterRotation;
				break;		
			case CharacterType.Barbarian:
				barbarian.Rotation =characterRotation;
				break;
			default:
				break;

		}
  }

	
}
