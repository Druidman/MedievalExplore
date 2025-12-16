@tool
extends MeshInstance3D

var cube_size = 1.0
func create_face(position: Vector3, direction: Vector3) -> Array[Vector3]:
	var vertices = []
	
	match direction:
		Vector3.UP:
			vertices = [
				position + Vector3(-0.5,  0.5, -0.5) * cube_size,
				position + Vector3( 0.5,  0.5, -0.5) * cube_size,
				position + Vector3( 0.5,  0.5,  0.5) * cube_size,
				position + Vector3(-0.5,  0.5,  0.5) * cube_size
			]
		Vector3.DOWN:
			vertices = [
				position + Vector3(-0.5, -0.5, 0.5) * cube_size,
				position + Vector3(0.5, -0.5, 0.5) * cube_size,
				position + Vector3(0.5, -0.5, -0.5) * cube_size,
				position + Vector3(-0.5, -0.5, -0.5) * cube_size
			]
		Vector3.LEFT:
			vertices = [
				position + Vector3(-0.5, -0.5, -0.5) * cube_size,
				position + Vector3(-0.5, 0.5, -0.5) * cube_size,
				position + Vector3(-0.5, 0.5, 0.5) * cube_size,
				position + Vector3(-0.5, -0.5, 0.5) * cube_size
		  	]	
		Vector3.RIGHT:
			vertices = [
				position + Vector3(0.5, -0.5, 0.5) * cube_size,
				position + Vector3(0.5, 0.5, 0.5) * cube_size,
				position + Vector3(0.5, 0.5, -0.5) * cube_size,
				position + Vector3(0.5, -0.5, -0.5) * cube_size
				]
		Vector3.FORWARD:
			vertices = [
				position + Vector3(-0.5, -0.5, -0.5) * cube_size,
				position + Vector3(0.5, -0.5, -0.5) * cube_size,
				position + Vector3(0.5, 0.5, -0.5) * cube_size,
				position + Vector3(-0.5, 0.5, -0.5) * cube_size
			]
		Vector3.BACK:
			vertices = [
				position + Vector3(-0.5, 0.5, 0.5) * cube_size,
				position + Vector3(0.5, 0.5, 0.5) * cube_size,
				position + Vector3(0.5, -0.5, 0.5) * cube_size,
				position + Vector3(-0.5,-0.5, 0.5) * cube_size
			]
	
	
	return [
		vertices[0], vertices[1], vertices[3],
		vertices[1], vertices[2], vertices[3],
	]
func createCube(position: Vector3) -> Array[Vector3]:
	var faces: Array[Vector3] = []
	faces.append_array(create_face(position, Vector3.UP))
	faces.append_array(create_face(position, Vector3.DOWN))
	faces.append_array(create_face(position, Vector3.LEFT))
	faces.append_array(create_face(position, Vector3.RIGHT))
	faces.append_array(create_face(position, Vector3.FORWARD))
	faces.append_array(create_face(position, Vector3.BACK))
	return faces
func generate_flat_world(vertices: PackedVector3Array) -> void:
	var positions = [
		Vector3(-0.5, 0.0, 0.5),
		Vector3(-0.5, 0.0, -0.5),
		Vector3(0.5, 0.0, -0.5),
		Vector3(0.5, 0.0, 0.5)
	]
	for pos in positions:
		vertices.append_array(createCube(pos))
			
func _ready() -> void:
	var vertices = PackedVector3Array()
	
	generate_flat_world(vertices)
	
	
	
	# Initialize the ArrayMesh.
	var arr_mesh = ArrayMesh.new()
	var arrays = []
	arrays.resize(Mesh.ARRAY_MAX)
	arrays[Mesh.ARRAY_VERTEX] = vertices
	
	# Create the Mesh.
	arr_mesh.add_surface_from_arrays(Mesh.PRIMITIVE_TRIANGLES, arrays)
	self.mesh = arr_mesh
