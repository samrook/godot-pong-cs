using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public float Speed { get; set; } = 500.0f;
	
	private float _halfHeight;

	public override void _Ready()
	{
		Sprite2D sprite = GetNode<Sprite2D>("Sprite2D");

		_halfHeight = (sprite.Texture.GetHeight() * sprite.Scale.Y) / 2.0f;
		
		GD.Print("Dynamic Half Height calculated: " + _halfHeight);
		GD.Print("Texture Height calculated: " + sprite.Texture.GetHeight());
	}

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

		// 3. Clamp between (0 + buffer) and (Screen - buffer)
		GlobalPosition = new Vector2(
			GlobalPosition.X, 
			Mathf.Clamp(GlobalPosition.Y, _halfHeight, screenHeight - _halfHeight)
		);
	}
}
