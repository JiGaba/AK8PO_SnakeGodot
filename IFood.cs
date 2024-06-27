using Godot;
using System;

public interface IFood
{
    Pixel Position { get; }
    void NextPosition();
}