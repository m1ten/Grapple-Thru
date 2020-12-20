using Godot;
using System;

public class Player : KinematicBody2D
{
    Vector2 up = new Vector2(0, -1);
    Vector2 motion = new Vector2();
    const int flap = 200;
    const int maxFallSpeed = 200;
    const int gravity = 10;
    PackedScene Wall = (PackedScene)GD.Load("res://WallNode.tscn");
    int score = 0;

    public override void _Ready()
    {

    }
    
    public override void _PhysicsProcess(float delta)
    {
        motion.y += gravity;
        if (motion.y > maxFallSpeed)
        {
            motion.y = maxFallSpeed;
        }

        if (Input.IsActionJustPressed("flap"))
        {
            motion.y = -flap;
        }

        motion = MoveAndSlide(motion, up);

        GetParent().GetParent().GetNode<RichTextLabel>("CanvasLayer/RichTextLabel").Text = score.ToString();
    }

    public void WallReset()
    {
        Node2D WallInstance = Wall.Instance() as Node2D;
        WallInstance.Position = new Vector2(900, DoubleToFloat(GD.RandRange(-120, 120)));
        GetParent().CallDeferred("add_child", WallInstance);
    }

    public static float DoubleToFloat(double value) 
    {
        return (float)value;
    }

    public void _on_Resetter_body_entered(Node Body)
    {
        if (Body.Name == "Walls")
        {
            Body.QueueFree();
            WallReset();
        }
    }

    public void _on_Detect_area_entered(Area Area)
    {
        if (Area.Name == "PointArea")
        {
            score++;
        }
    }

    public void _on_Detect_body_entered(Node Body)
    {
        if (Body.Name == "Walls")
        {
            GetTree().ReloadCurrentScene();
        }
    }
}
