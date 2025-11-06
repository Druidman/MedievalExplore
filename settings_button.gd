extends Button
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	position = (get_viewport_rect().size - size) / 2 + Vector2(0, 0)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass


func _on_settings_button_pressed() -> void:
	get_tree().change_scene_to_file("res://settings.tscn")
