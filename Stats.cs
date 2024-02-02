using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class Stats : Resource {
	[Export] 
	public float Coins { get; set; }
	[Export] 
	public float Health { get; set; }
	[Export] 
	public float MovementSpeed { get; set; }
	[Export] 
	public float JumpVelocity { get; set; }

	// Stat Buff Algorithm. //

	// Add a dictionary to store currently active buffs applied to the player (or enemy) 
	private Dictionary<string, float> activeBuffs = new Dictionary<string, float>();

	// declare our timer
	private System.Timers.Timer timer = new System.Timers.Timer();

	// Implement a function that applies a specified stat buff and resets after a specified time has elapsed. 
	public void ApplyStatBuff (
		string statToBuff, 
		float buffAmount, 
		float duration, 
		float baseStat = 0
		)
	{
		float _baseStat1 = baseStat;

		timer.AutoReset = false; // set this to false so it doesn't fuck everything up like it likes to do.
		// if a buff is already active, update its duration.
		if (activeBuffs.ContainsKey(statToBuff))
		{
			activeBuffs[statToBuff] = duration;
			GD.Print($"\n ApplyStatBuff buff {statToBuff} already active. Reset duration back to {duration}. \n"); 
		}
		else
		{
			activeBuffs.Add(statToBuff, duration); // Apply the new buff 

			// Apply the stat buff logic here. 
			switch (statToBuff) // Increase the stat by buffAmount
			{
				case "health":
					{
						Health += buffAmount; 
						break;
					}
				case "movement speed":
					{
						MovementSpeed += buffAmount;
						break;
					}
				case "jump velocity":
					{
						JumpVelocity += buffAmount;
						break;
					}
				default: { break; }
			}

			GD.Print($"\n ApplyStatBuff Applied a {buffAmount} buff to the {statToBuff} stat for {duration} seconds. \n");
		}

		// Start or reset the timer.
		// The code will skip over timer.Stop() if a timer has never been set. 
		// If there is an active timer from a previous buff, it is stopped to avoid conflicts.
		timer.Stop();

		// Convert seconds to milliseconds by multiplying the float by 1000. 
		timer.Interval = duration * 1000; 

		// Add an event handler to the Elapsed event, specifying that when the timer elapses,
		// the ResetStatBuff method should be called with the statToBuff parameter.
		timer.Elapsed += (sender, e) => ResetStatBuff(statToBuff, baseStat);

		// Start the timer with the new configuration.
		timer.Start();
	}

	// function to reset the stat buff.
	private void ResetStatBuff(string statToReset, float baseStat)
	{
		float resetStatValue = 0.0f;
		// Reset logic here
		switch (statToReset)
		{
			case "health":
				{
					// we wanna make sure that the player doesn't recieve a permanent base health boost.
					// make that shit temporary.
					if (Health >= baseStat)
						Health = baseStat;
					resetStatValue = Health; 
					break;
				}
			case "movement speed":
				{
					MovementSpeed = baseStat; 
					resetStatValue = MovementSpeed;
					break;
				}
			case "jump velocity":
				{
					JumpVelocity = baseStat;
					resetStatValue = JumpVelocity;
					Math.Abs(JumpVelocity); // this is because jumpVelocity is typically negative.
					break;
				}
			default: { break; }
		}
		GD.Print($"\n ResetStatBuff Reset the {statToReset} buff.\n {statToReset} is back to {resetStatValue.ToString()}");
		activeBuffs.Remove(statToReset); // Remove the expired buff from the active buffs dictionary.
		timer.Stop(); // Stop the timer to avoid further resets for this stat.
	}
}
