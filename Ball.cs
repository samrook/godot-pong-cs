using Godot;
using System;

public partial class Ball : CharacterBody2D
{
	[Export]
	public float Speed { get; set; } = 700.0f;
	
	private Vector2 _direction = Vector2.Zero;
	
	public override void _Ready()
	{
		ResetDirection();
	}

	public override void _PhysicsProcess(double delta)
	{
		// 2. Calculate the movement for this frame
		// Velocity = Direction * Speed * Delta Time
		Vector2 velocity = _direction * Speed * (float) delta;

		// 3. Move and Check for Collisions
		// MoveAndCollide moves the body. If it hits something, it returns data.
		// If it hits nothing, it returns null.
		KinematicCollision2D collision = MoveAndCollide(velocity);
		
		if (collision != null)
		{
			// GetNormal() tells us the angle of the wall we hit (e.g., pointing UP or LEFT)
			// The Bounce() function does the vector math for us!
			_direction = _direction.Bounce(collision.GetNormal());
		}
	}
	
	public void Reset(Vector2 startPosition)
	{
		// 1. Teleport back to the middle
		GlobalPosition = startPosition;
		
		ResetDirection();
	}
	
	private bool ShouldFireRight()
	{
		Random random = new Random();
		
		return random.NextDouble() < 0.5;
	}
	
	private void ResetDirection()
	{
		// 1. Randomize the direction slightly so it's not boring
		// (We pick a random angle between -45 and +45 degrees roughly
		float randomY = (float)GD.RandRange(-0.5, 0.5);
		bool shouldFireRight = ShouldFireRight();
		
		// 2. Set the direction.
		// .Normalized() ensures the vector length is always 1 (consistent speed)
		_direction = new Vector2(shouldFireRight ? -1 : 1, randomY).Normalized();
	}
}
