
extends Camera3D

const movementRadius: int = 15
const cameraOffset = Vector3(0, 10,15)
var angle: float = 0.0


@onready var player = get_tree().current_scene.get_node("Player")

func update_camera():
	
	var pivot = player.global_position
	var offset = cameraOffset
	

	offset = offset.rotated(Vector3.UP, angle)
	

	global_position = pivot + offset
	global_rotation.y = angle
	

	

func _ready() -> void:
	
	look_at_from_position(position, player.global_position)
	update_camera()
	

func _input(event: InputEvent):
	if event is InputEventMouseMotion:

		angle -= event.relative.x * 0.01  
	
		

func _process(delta: float) -> void: 
	print(player.global_position)
	update_camera()
	
	
		
	
	
