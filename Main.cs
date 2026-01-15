using Godot;
using System;

public partial class Main : Node2D
{
	// We need references to our objects
	private Ball _ball;
	private Area2D _leftGoal;
	private Area2D _rightGoal;
	
	// Starting position to reset to (Screen Center)
	private Vector2 _startPosition;
	
	// UI elements
	private Label _scoreLabel;
	private int _leftScore = 0;
	private int _rightScore = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_ball = GetNode<Ball>("Ball");
		_leftGoal = GetNode<Area2D>("LeftGoal");
		_rightGoal = GetNode<Area2D>("RightGoal");
		_scoreLabel = GetNode<Label>("HUD/ScoreLabel");
		
		_startPosition = GetViewportRect().Size / 2;
		
		_leftGoal.BodyEntered += OnLeftGoalEntered;
		_rightGoal.BodyEntered += OnRightGoalEntered;
	}
	
	private void OnLeftGoalEntered(Node2D body)
	{
		// Ensure it's actually the ball (and not a player glitching through wall)
		if (body is Ball)
		{
			_rightScore++;
			UpdateScoreUI();
			_ball.Reset(_startPosition);
			GD.Print("Right Player Scores!");
		}
	}

	private void OnRightGoalEntered(Node2D body)
	{
		if (body is Ball)
		{
			_leftScore++;
			UpdateScoreUI();
			_ball.Reset(_startPosition);
			GD.Print("Left Player Scores!");
		}
	}
	
	private void UpdateScoreUI()
	{
		_scoreLabel.Text = $"{_leftScore} - {_rightScore}";
	}
}
