class_name Scene_model extends Node

var path: String = "none.tscn"

func _init(p_path: String = "none.tscn"):
	path = p_path

func _input(event: InputEvent) -> void:
	if event.is_action_pressed("return"):
		if path == "none.tscn":
			return
		get_tree().change_scene_to_file("res://" + path)
	
