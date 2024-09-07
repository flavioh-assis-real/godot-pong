using Godot;

public partial class Player2AI : CharacterBody2D
{
    const float Speed = 500f;
    Node2D Game;
    CharacterBody2D Ball;

    public override void _Ready()
    {
        this.Game = GetNode<Node2D>("/root/Game");
        this.Ball = GetNode<CharacterBody2D>("../Ball");
    }

    public override void _PhysicsProcess(double delta)
    {
        this.Velocity = GetVelocityFromBallPosition();
        MoveAndCollide(this.Velocity * (float)delta);
    }

    private Vector2 GetVelocityFromBallPosition()
    {
        if (MustResetPosition())
            return ResetPosition();

        var ball = GetNode<CharacterBody2D>("../Ball");

        if (ball is null)
            return new Vector2(0, 0);

        var ballPosition = ball.GlobalPosition;

        if (ballPosition.Y > this.GlobalPosition.Y)
            return new Vector2(0, Speed);

        return new Vector2(0, -Speed);
    }

    private bool MustResetPosition()
    {
        return !(bool)this.Game.Get("IsRunning") || !(bool)this.Ball.Get("IsBallMovingTowardsAI");
    }

    private Vector2 ResetPosition()
    {
        if (this.GlobalPosition.Y > 360)
            return new Vector2(0, -Speed);

        if (this.GlobalPosition.Y < 350)
            return new Vector2(0, Speed);

        return new Vector2(0, 0);
    }
}
