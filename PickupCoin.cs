using Godot;
using System; 

public partial class PickupCoin : Area2D
{
	// Get a reference to Player2D 
	Player2D player;

	// Get a reference to AnimatedSprite2D
	private AnimatedSprite2D coinSprite;
	public override void _Ready()
	{
		// I am putting the try .. catch statement here for peace of mind and because it's cool
		try
		{
			// initialize our references
			player = GetNode<Player2D>("../Player2D");
			coinSprite = GetNode<AnimatedSprite2D>("CoinSprite");
		} catch (Exception ex) 
		{
			// print the exception if something horrible happens
			GD.PrintErr(ex.ToString()); 
		}
	}
	public override void _PhysicsProcess(double delta) => coinSprite.Play("idle"); 
	private void OnCoinPickup2(Node2D body) => OnCoinPickup(body);
	private void OnCoinPickup(Node2D body)
	{
		GD.Print("\n OnCoinPickup Detect body: " + body.ToString() + "\n"); // FOR DEBUG

		player.IterateCoins(1);
		player.DisplayStats("coins"); // FOR DEBUG 
		QueueFree(); 
	}
	
}
