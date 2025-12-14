
extends CharacterBody3D


const SPEED = 10.0
const DECELERATION_SPEED = SPEED * 0.1


const JUMP_FORCE = 10 
const GRAVITY_SPEED = 20

var camera: Camera3D
var upward_force = 0

func _ready() -> void:
	print("player ready")
	camera = get_tree().current_scene.get_node("Camera")

func _physics_process(delta: float) -> void:
	rotation.y = camera.global_rotation.y
	
	if is_on_floor():
		velocity.y = 0
	else:
		velocity.y -= GRAVITY_SPEED * delta
	
	var move_vec = Vector3(0,0,0)
	if Input.is_action_just_pressed("move_up") and is_on_floor():
		velocity.y = JUMP_FORCE
	
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
		
#
	#upward_force = move_toward(upward_force, 0, (GRAVITY_FACTOR/100.0) * upward_force)
	#
	#velocity.y = move_vec.y + upward_force
	
	move_and_slide()
