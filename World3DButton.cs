using Godot;
using System;

public partial class World3DButton : Area3D
{
	[Export] 
	MeshInstance3D textMesh;

	private Color? originalColor = null; // Store the color here



	public void hovered()
	{

		MakeMeshGreyish(0.7f); // Make it darker/greyer on hover
	}

	public void unhovered()
	{
	
		RevertColor();

	}

	public void MakeMeshGreyish(float amount = 0.5f)
	{
		// Try to get the active material
		var material = textMesh.GetActiveMaterial(0) as StandardMaterial3D;
		
		if (material == null)
		{
			GD.PrintErr("Material is null! Make sure the MeshInstance3D has a material assigned.");
			return;
		}

		if (originalColor == null)
		{
			originalColor = material.AlbedoColor;
		}

		// IMPORTANT: For TextMesh, we want to ensure we are overriding the surface
		StandardMaterial3D uniqueMat = textMesh.GetSurfaceOverrideMaterial(0) as StandardMaterial3D;
		
		if (uniqueMat == null)
		{
			uniqueMat = (StandardMaterial3D)material.Duplicate();
			// Force the material to be unique to THIS instance
			textMesh.SetSurfaceOverrideMaterial(0, uniqueMat);
		}

		Color original = originalColor.Value;
		Color grayTarget = new Color(original.V - 0.5f, original.V - 0.5f, original.V - 0.5f);
		
		// Apply the color change
		uniqueMat.AlbedoColor = original.Lerp(grayTarget, amount);
		

	}

	public void RevertColor()
	{
		// Only revert if we actually saved a color and have a material override
		GD.Print("revert");
		GD.Print(originalColor);
		if (originalColor != null && textMesh.GetSurfaceOverrideMaterial(0) is StandardMaterial3D mat)
		{
			
			mat.AlbedoColor = originalColor.Value;
		}
	}
}
