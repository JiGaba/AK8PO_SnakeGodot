using Godot;
using System;
using System.Collections.Generic;

public interface ISnake
{
    Pixel Head {  get; }
    List<Pixel> Body { get; }
    void IncreasePixels();
    int Length();
    void Increase();
    bool IsCrashInto();
    bool IsFoodEaten(Pixel food);
    bool IsBitHimself(int index);
    void Move(Direction movement);
}