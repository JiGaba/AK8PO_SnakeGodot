using Godot;
using System;

public class Food : IFood
{
    public Pixel Position { get; set; }

    private readonly Random _randomGenerator;
    private int _screenHeight;
    private int _screenWidth;

    public Food(int screenWidth, int screenHeight)
    {
        _screenHeight = screenHeight;
        _screenWidth = screenWidth;
        _randomGenerator = new Random();

        NextPosition();
    }

    public void NextPosition()
    {
        Position = new Pixel(
            _randomGenerator.Next(0, _screenWidth), 
            _randomGenerator.Next(1, _screenHeight),
            new Color(1,0,0));
    }
}