using Godot;
using System;
public partial class PlayerGUIControl : Control
{
	// we wanna get our player. 
	Player2D _player;

	// This is the label that will keep count of the coins collected
	private Label _coinLabel;

	// This is the label for health
	private Label _healthLabel;

	// This is the camera
	private Camera2D _camera;

	// add others here if necissary
	public override void _Ready()
	{
		try
		{
			_camera = GetNode<Camera2D>("../../Camera2D");
			_coinLabel = GetNode<Label>("CoinCollectLabel");
			_player = GetNode<Player2D>("../../../../Player2D");
			_healthLabel = GetNode<Label>("HealthLabel");
		}
		catch (Exception ex)
		{
			GD.PrintErr(ex.ToString());
		}
		_camera.MakeCurrent();
	}
	public override void _Process(double delta)
	{
		// Set the label text to the current score.
		_coinLabel.Text = "Coins: " + _player.ReturnStatValue("coins");

		// Set the label text to the players' current health value.
		_healthLabel.Text = "Health: " + _player.ReturnStatValue("health");
	}
}
