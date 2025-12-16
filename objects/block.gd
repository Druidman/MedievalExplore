class_name Block

var position: Vector3
var size: float # remove later for memory optimization

func _init(block_pos: Vector3, block_size: float) -> void:
	self.position = block_pos
	self.size = block_size


func make_full_block_mesh() -> Array[Vector3]:
	var faces: Array[Vector3] = []

	faces.append_array(self.create_face(Vector3.UP))
	faces.append_array(self.create_face(Vector3.DOWN))
	faces.append_array(self.create_face(Vector3.LEFT))
	faces.append_array(self.create_face(Vector3.RIGHT))
	faces.append_array(self.create_face(Vector3.FORWARD))
	faces.append_array(self.create_face(Vector3.BACK))

	return faces

func create_face(direction: Vector3) -> Array[Vector3]:
	var vertices = []
	
	match direction:
		Vector3.UP:
			vertices = [
				self.position + Vector3(-0.5,  0.5, -0.5) * self.size,
				self.position + Vector3( 0.5,  0.5, -0.5) * self.size,
				self.position + Vector3( 0.5,  0.5,  0.5) * self.size,
				self.position + Vector3(-0.5,  0.5,  0.5) * self.size
			]
		Vector3.DOWN:
			vertices = [
				self.position + Vector3(-0.5, -0.5, 0.5) * self.size,
				self.position + Vector3(0.5, -0.5, 0.5) * self.size,
				self.position + Vector3(0.5, -0.5, -0.5) * self.size,
				self.position + Vector3(-0.5, -0.5, -0.5) * self.size
			]
		Vector3.LEFT:
			vertices = [
				self.position + Vector3(-0.5, -0.5, -0.5) * self.size,
				self.position + Vector3(-0.5, 0.5, -0.5) * self.size,
				self.position + Vector3(-0.5, 0.5, 0.5) * self.size,
				self.position + Vector3(-0.5, -0.5, 0.5) * self.size
		  	]	
		Vector3.RIGHT:
			vertices = [
				self.position + Vector3(0.5, -0.5, 0.5) * self.size,
				self.position + Vector3(0.5, 0.5, 0.5) * self.size,
				self.position + Vector3(0.5, 0.5, -0.5) * self.size,
				self.position + Vector3(0.5, -0.5, -0.5) * self.size
				]
		Vector3.FORWARD:
			vertices = [
				self.position + Vector3(-0.5, -0.5, -0.5) * self.size,
				self.position + Vector3(0.5, -0.5, -0.5) * self.size,
				self.position + Vector3(0.5, 0.5, -0.5) * self.size,
				self.position + Vector3(-0.5, 0.5, -0.5) * self.size
			]
		Vector3.BACK:
			vertices = [
				self.position + Vector3(-0.5, 0.5, 0.5) * self.size,
				self.position + Vector3(0.5, 0.5, 0.5) * self.size,
				self.position + Vector3(0.5, -0.5, 0.5) * self.size,
				self.position + Vector3(-0.5,-0.5, 0.5) * self.size
			]
	
	
	return [
		vertices[0], vertices[1], vertices[3],
		vertices[1], vertices[2], vertices[3],
	]
