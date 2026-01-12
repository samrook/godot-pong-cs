using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public float Speed { get; set; } = 400.0f;

	public override void _PhysicsProcess(double delta)
	{
		// GetInput() returns a Vector2 (X, Y).
		// If we press Up, Y is -1. Down, Y is 1.
		// We only care about Y for Pong.
		float direction = Input.GetAxis("move_up", "move_down");

		// Set the built-in Velocity property
		Velocity = new Vector2(0, direction * Speed);

		// MoveAndSlide uses 'Velocity' to move the body
		MoveAndSlide();
		
		// 1. Get the screen size
		float screenHeight = GetViewportRect().Size.Y;

		// 2. Define a "buffer" (half the sprite height). 
		// The Godot icon is roughly 128px, so half is 64.
		// You can adjust this number until it looks right.
		float halfHeight = 64.0f; 

		// 3. Clamp between (0 + buffer) and (Screen - buffer)
		GlobalPosition = new Vector2(
			GlobalPosition.X, 
			Mathf.Clamp(GlobalPosition.Y, halfHeight, screenHeight - halfHeight)
		);
	}
}
