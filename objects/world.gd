@tool
extends MeshInstance3D

var block_size = 1.0



var world: Array[Block]



func generate_world() -> void:
	var positions = [
		Vector3(-0.5, 0.0, 0.5),
		Vector3(-0.5, 0.0, -0.5),
		Vector3(0.5, 0.0, -0.5),
		Vector3(0.5, 0.0, 0.5)
	]
	for pos in positions:
		world.append(Block.new(pos, block_size)) #Yes still using block_size TODO remove it

func generate_world_mesh(vertices: PackedVector3Array) -> void:
	for block in self.world:
		vertices.append_array(block.make_full_block_mesh()) # For now just to see it work
	
		
			
func _ready() -> void:
	var vertices = PackedVector3Array()
	
	generate_world()

	generate_world_mesh(vertices)
	
	
	
	# Initialize the ArrayMesh.
	var arr_mesh = ArrayMesh.new()
	var arrays = []
	arrays.resize(Mesh.ARRAY_MAX)
	arrays[Mesh.ARRAY_VERTEX] = vertices
	
	# Create the Mesh.
	arr_mesh.add_surface_from_arrays(Mesh.PRIMITIVE_TRIANGLES, arrays)
	self.mesh = arr_mesh
