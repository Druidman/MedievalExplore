extends Camera3D

const movementRadius: int = 25
var angle: float = 0.0


@onready var player = get_tree().get_first_node_in_group("player")

func update_camera():
	var pivot = player.global_position
	var offset = Vector3(0, 15, movementRadius)
	

	offset = offset.rotated(Vector3.UP, angle)
	

	global_position = pivot + offset
	look_at(pivot)
	

func _ready() -> void:
	update_camera()
	

func _input(event: InputEvent):
	if event is InputEventMouseMotion:

		angle -= event.relative.x * 0.01  
	
		

func _process(delta: float) -> void:
	update_camera()
	
	
		
	
	
