using Godot;
using System;
using System.Collections.Generic; 

public partial class Player2D : CharacterBody2D
{
	[Export]
	Stats playerStats;

	// 300
	private float Speed = 300.0f; 
	
	// -400.0f
	private float JumpVelocity = -400.0f;

	// 100
	private float Health = 100; 

	private AnimatedSprite2D _animatedSprite; 
	
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public void IterateCoins(int amount) => playerStats.Coins += amount;

	public void IterateHealth(int amount) => playerStats.Health += amount;
	// FOR DEBUG ONLY!!!
	public void DisplayStats(string statToDisplay) {
		switch(statToDisplay) {
			case "health": {
				GD.Print(playerStats.Health.ToString() + "\n");
				break;
				}
			case "coins": {
				GD.Print(playerStats.Coins.ToString() + "\n");
				break;
				}
			default: { break; }
		}
	}

	public string ReturnStatValue(string statName)
	{
		string returnStat = " ";
		switch(statName)
		{
			case "health": {
					returnStat = playerStats.Health.ToString();
					break;
				}
			case "coins": { 
					returnStat = playerStats.Coins.ToString();
					break;
				}
			default: { break; }
		}
		return returnStat;
	}

	// this will work for now. I will prob find a better way to do this later.
	public void BuffPlayerStat(string statName, float buffAmount, float duration)
	{
		// THIS IS HIGHLY BETA, PLEASE BEAR WITH ME 
		// these base stats will help reset this shit 
		float baseSpeed = 300.0f;
		float baseHealth = 100; 
		float baseJumpVelocity = -400.0f;

		switch (statName)
		{
			case "health": {
					playerStats.ApplyStatBuff(statName, buffAmount, duration, baseHealth);
					break;
				}
			case "movement speed": {
					playerStats.ApplyStatBuff(statName, buffAmount, duration, baseSpeed);
					break;
				}
			case "jump velocity": {
					playerStats.ApplyStatBuff(statName, buffAmount, duration, baseJumpVelocity);
					break;
				}
			default: { break; }
		}
	}

	public override void _Ready() => _animatedSprite = GetNode<AnimatedSprite2D>("PlayerSprite");

	// this, and everything in it, is called every tick. 
	public override void _PhysicsProcess(double delta)
	{
		// I want to set the current speed, health and jump values here to the speed in our Stats.
		// I am also putting them here so they are updated every tick
		// (or frame...? idk they're updated very fast depending on the speed of the computer.)
		Speed = playerStats.MovementSpeed;
		Health = playerStats.Health;
		JumpVelocity = playerStats.JumpVelocity;

		Vector2 direction = Input.GetVector("Left", "Right", "Jump", "Down");

		Vector2 velocity = Velocity;

		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;
		if (Input.IsActionJustPressed("Jump") && IsOnFloor())
			velocity.Y = JumpVelocity;

		if (direction != Vector2.Zero)
		{
			//GD.Print(direction.ToString() + "\n"); // FOR DEBUG
			_animatedSprite.Play("moving");

			if (direction == Vector2.Left)
				_animatedSprite.FlipH = true;
			else if (direction == Vector2.Right)
				_animatedSprite.FlipH = false;

			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			_animatedSprite.Play("idle"); 
		}
		Velocity = velocity;
		MoveAndSlide();
	}
}
