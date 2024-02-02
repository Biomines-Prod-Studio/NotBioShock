using Godot;
using System;

public partial class Powerup_Speed : Area2D
{
	// Get a reference to Player2D
	Player2D player; 

	// we don't need to reference AnimatedSprite2D because the sprite for powerups are not animated

	public override void _Ready()
	{
		// Peace of mind once again
		try 
		{ 
			// Initialize our player and get a reference to the node itself.
			player = GetNode<Player2D>("../Player2D");
		} catch (Exception ex)
		{
			// print the exception if something horrible happens
			GD.PrintErr(ex.ToString());
		}
	}

	public override void _Process(double delta) { }
	private void OnPowerupPickup(Node2D body)
	{
		GD.Print("\n OnPowerupPickup (Speed) Detect body: " + body.ToString() + "\n"); // FOR DEBUG

		// buff the player's speed by 100 for 5 seconds
		player.BuffPlayerStat("movement speed", 100, 5); 

		// For now, just delete it 
		QueueFree(); 
	}
}
