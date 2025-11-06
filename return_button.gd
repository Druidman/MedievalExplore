extends Button


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	size.x = 100
	size.y = 50
	position = (get_viewport_rect().size - size) / 2
	text = "RETURN"
	


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass


func _on_pressed() -> void:
	get_tree().change_scene_to_file("res://main.tscn")
