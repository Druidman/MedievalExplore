extends Scene_model 
#
#
#
func _ready():
	
	path = "app_scenes/main/main.tscn"
	Input.mouse_mode = Input.MOUSE_MODE_CAPTURED
	
func _input(event: InputEvent) -> void:
	super._input(event)
	if event.is_action_pressed("return"):
		Input.mouse_mode = Input.MOUSE_MODE_VISIBLE
