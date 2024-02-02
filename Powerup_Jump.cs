using Godot;
using System;

public partial class Powerup_Jump : Area2D
{

	Player2D player;

	public override void _Ready()
	{
		try
		{
			player = GetNode<Player2D>("../Player2D");
		}
		catch (Exception ex)
		{
			GD.PrintErr(ex.ToString());
		}
	}

	// Nothing in here YET.......
	public override void _Process(double delta) { }
	private void OnPowerupPickup(Node2D body)
	{
		GD.Print("\n OnPowerupPickup (Jump) Detect Body: " + body.ToString() + "\n");

		player.BuffPlayerStat("jump velocity", -200, 2);

		// Delete it 
		QueueFree(); 

	}
}
