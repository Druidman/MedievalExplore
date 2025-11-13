extends CSGBox3D


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	add_to_group("player")
	


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	
	
	if (Input.is_action_pressed("move_left")):
		global_position.x -= 10*delta
	
	if (Input.is_action_pressed("move_right")):
		global_position.x += 10*delta
		
	if (Input.is_action_pressed("move_front")):
		global_position.z -= 10*delta
		
	if (Input.is_action_pressed("move_back")):
		global_position.z += 10*delta
		
	print(global_position, " " , position)
