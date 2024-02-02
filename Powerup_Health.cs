using Godot;
using System;

public partial class Powerup_Health : Area2D
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
	public override void _Process(double delta) { }

	private void OnPowerupPickup(Node2D body)
	{
		GD.Print("\n OnPowerupPickup (Health) Detect body: " + body.ToString() + "\n"); // FOR DEBUG

		player.BuffPlayerStat("health", 100, 5);

		QueueFree(); 
	}
}
