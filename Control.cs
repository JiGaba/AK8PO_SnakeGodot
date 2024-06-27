using Godot;
using System;

public partial class Control : Godot.Control
{
	private const int _screenHeight = 16;
	private const int _screenWidth = 16;
	private const int _timerMax = 20;

	private ISnake _snake;
	private IFood _food;
	private Label _scoreLabel;
	private Button _startButton;

	private ButtonPressed _buttonPressed;
	private Direction _movement;
	private int _score;
	private bool _gameOver; 
	private int _timer;

	public override void _Ready()
	{
		InitializeScene();

		_scoreLabel = GetNode<Label>("../Panel/Label");
		_startButton = GetNode<Button>("../Panel/Button");

		_startButton.Pressed += OnStartButtonPressed;
	}

	private void InitializeScene()
	{
		_score = 5;
		_timer = 0;
		_gameOver = false;
		_movement = Direction.Right;
		_buttonPressed = ButtonPressed.No;

		_snake = new Snake(_score, _screenWidth, _screenHeight);
		_food = new Food(_screenWidth, _screenHeight);	
	}

	private void OnStartButtonPressed()
	{
		InitializeScene();
	}

	public override void _Process(double delta)
	{	
		KeyPressed();
		
		_timer++;
		if(_timer == _timerMax && !_gameOver){
			_timer = 0;
			_buttonPressed = ButtonPressed.No;
				
			if (_snake.IsCrashInto()) _gameOver = true;
			if (_snake.IsFoodEaten(_food.Position)) FoodIsEaten();

			for (int i = 0; i < _snake.Length(); i++)
				if (_snake.IsBitHimself(i)) _gameOver = true;
				
			if(!_gameOver)QueueRedraw();	

			_snake.Increase();
			_snake.Move(_movement);

			LabelTextChange();
		}
	}
	
	public override void _Draw()
	{
		for (int i = 0; i < _snake.Length(); i++)
			DrawRect(_snake.Body[i].Rect, _snake.Body[i].ScreenColor);

		DrawRect(_food.Position.Rect, _food.Position.ScreenColor);
		DrawRect(_snake.Head.Rect, _snake.Head.ScreenColor);

		DrawRect(new Rect2(0,0,800,50),new Color(1,1,0));
	}

	private void KeyPressed()
	{
		if (Input.IsActionPressed("ui_up") && _movement != Direction.Down && _buttonPressed == ButtonPressed.No)
			SetupDirection(Direction.Up);
		
		if (Input.IsActionPressed("ui_down") && _movement != Direction.Up && _buttonPressed == ButtonPressed.No)
			SetupDirection(Direction.Down);
		
		if (Input.IsActionPressed("ui_right") && _movement != Direction.Left && _buttonPressed == ButtonPressed.No)
			SetupDirection(Direction.Right);

		if (Input.IsActionPressed("ui_left") && _movement != Direction.Right && _buttonPressed == ButtonPressed.No)
			SetupDirection(Direction.Left);
	}

	private void FoodIsEaten()
	{
		_score++;
		_snake.IncreasePixels();
		_food.NextPosition();
	}

	private void SetupDirection(Direction direction) 
	{
		_movement = direction;
		_buttonPressed = ButtonPressed.Yes;	
	}

	private void LabelTextChange()
	{
		if(_gameOver)
		{
			_scoreLabel.Text = "Game over! Your score: " + 
			_score.ToString();
		}
		else
		{
			_scoreLabel.Text = "Score: " + _score.ToString();
		}
	}
}
