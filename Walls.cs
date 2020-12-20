using Godot;
using System;

public class Walls : StaticBody2D
{
    public override void _Ready()
    {
        
    }
    public override void _PhysicsProcess(float delta)
    {
        Position += new Vector2(-2, 0);
    }
}
