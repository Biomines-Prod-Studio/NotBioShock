extends CharacterBody2D

var health = 10
const SPEED = 300.0
const JUMP_VELOCITY = -400.0
const DOUBLE_JUMP = true
var can_double_jump = true

const DASH_SPEED = 600.0  # Dash speed, adjust as needed.
const DASH_DURATION = 0.2  # Dash duration in seconds.
const DASH_COOLDOWN = 1.0  # Cooldown period between dashes in seconds.
var can_dash = true  # To check if the player can dash.
var dashing = false  # To check if the player is currently dashing.
var dash_timer = 0.0  # To track the duration of the dash.
var dash_cooldown_timer = 0.0  # To track the cooldown period between dashes.

var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")

@onready var anim = get_node("AnimationPlayer")

@export var inv = Inv

func _physics_process(delta):
	# Add the gravity.
	if not is_on_floor():
		velocity.y += gravity * delta

	# Handle jump.
	if Input.is_action_just_pressed("ui_accept"):
		if is_on_floor() or (can_double_jump and DOUBLE_JUMP):
			if not is_on_floor():
				can_double_jump = false
			velocity.y = JUMP_VELOCITY
			anim.play("Jump")

	# Handle dash.
	if Input.is_action_just_pressed("ui_dash") and can_dash and not dashing:
		start_dash()

	# Update dash.
	if dashing:
		dash_timer -= delta
		if dash_timer <= 0:
			end_dash()
	else:
		dash_cooldown_timer -= delta
		if dash_cooldown_timer <= 0:
			can_dash = true

	# Handle movement.
	var direction = Input.get_axis("ui_left", "ui_right")
	if not dashing:  # Skip movement handling if dashing.
		handle_movement(direction, delta)

	move_and_slide()

	if is_on_floor():
		can_double_jump = true

	if health <= 0:
		queue_free()
		get_tree().change_scene_to_file("res://main.tscn")

func handle_movement(direction, delta):
	if direction == -1:
		get_node("AnimatedSprite2D").flip_h = true
	elif direction == 1:
		get_node("AnimatedSprite2D").flip_h = false
	if direction and not dashing:
		velocity.x = direction * SPEED
		if velocity.y == 0:
			anim.play("Run")
	else:
		velocity.x = move_toward(velocity.x, 0, SPEED)
		anim.play("Idle")

	if velocity.y < 0:
		anim.play("Jump")
	if velocity.y > 0:
		anim.play("Fall")

func start_dash():
	dashing = true
	can_dash = false
	dash_timer = DASH_DURATION
	dash_cooldown_timer = DASH_COOLDOWN
	velocity.x = DASH_SPEED * (-1 if get_node("AnimatedSprite2D").flip_h else 1)
	#anim.play("Dash")

func end_dash():
	dashing = false
