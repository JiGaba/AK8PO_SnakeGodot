using Godot;
using System;

public class Pixel
{
    private const int _pixelSize = 50;
    public int XPos { get; set; }
    public int YPos { get; set; }
    public int XPosDraw { get { return XPos * _pixelSize;}}
    public int YPosDraw { get { return YPos * _pixelSize;}}
    public Rect2 Rect { get { return new Rect2(XPosDraw, YPosDraw, _pixelSize, _pixelSize);} }
    public Color ScreenColor { get; set; }

    public Pixel(int xPos, int yPos, Color screenColor)
    {
        XPos = xPos;
        YPos = yPos;
        ScreenColor = screenColor; 
    }

}