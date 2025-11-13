extends Button


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	position = (get_viewport_rect().size - size) / 2 + Vector2(0, 100)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	position = (get_viewport_rect().size - size) / 2 + Vector2(0, 100)

func _on_leave_button_pressed() -> void:
	get_tree().quit() 
