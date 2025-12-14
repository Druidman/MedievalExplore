
extends CharacterBody3D


const SPEED = 10.0
const DECELERATION_SPEED = SPEED * 0.1
const JUMP_VELOCITY = 4.5

var camera: Camera3D

func _ready() -> void:
	print("player ready")
	camera = get_tree().current_scene.get_node("Camera")

func _physics_process(_delta: float) -> void:
	rotation.y = camera.global_rotation.y
	
	var move_vec = Vector3(0,-1,0)
	if Input.is_action_pressed("move_front"):
		move_vec.z += -1
	if Input.is_action_pressed("move_back"):
		move_vec.z += 1
	if Input.is_action_pressed("move_left"):
		move_vec.x += -1
	if Input.is_action_pressed("move_right"):
		move_vec.x += 1
		
	move_vec *= SPEED
		
	
	move_vec = move_vec.rotated(Vector3.UP, rotation.y)
	
	
	
	if move_vec.z:
		velocity.z = move_vec.z
		
	else:
		velocity.z = move_toward(velocity.z, 0, DECELERATION_SPEED)
		
	if move_vec.x:
		velocity.x = move_vec.x
	else:
		
		velocity.x = move_toward(velocity.x, 0, DECELERATION_SPEED)
		
	velocity.y = move_vec.y
	
	move_and_slide()
