using Godot;

public partial class Ball : CharacterBody2D
{
    int Speed = 300;
    private bool IsBallMovingTowardsAI = false;
    Vector2 Direction;

    public override void _Ready()
    {
        this.Direction = new Vector2(0, 0);
    }

    public override void _PhysicsProcess(double delta)
    {
        var initialGlobalPositionX = this.GlobalPosition.X;

        var collision = MoveAndCollide(this.Direction * (float)delta);

        this.IsBallMovingTowardsAI = CheckBallDirecion(initialGlobalPositionX);

        if (collision is null) return;

        this.ChangeDirection(collision);
    }

    public void Start()
    {
        this.Direction = GetInitialDirection();
    }

    private Vector2 GetInitialDirection()
    {
        int X = GetRandomAxisDirection();
        int Y = GetRandomAxisDirection();

        return new Vector2(X * Speed, Y * Speed);
    }

    private static int GetRandomAxisDirection()
    {
        return GD.RandRange(0, 1) == 0 ? -1 : 1;
    }

    private bool CheckBallDirecion(float initialGlobalPositionX)
    {
        return this.GlobalPosition.X > initialGlobalPositionX;
    }

    private void ChangeDirection(KinematicCollision2D collision)
    {
        if (IsCollidingWithGroup(collision, "player"))
        {
            if (IsCollidingOnTopOrBottom(collision))
                this.Direction.Y *= -1 * 10f;
            else
                this.Direction.X *= -1 * 1.10f;

        }
        if (IsCollidingWithGroup(collision, "wall"))
            this.Direction.Y *= -1;
    }

    private static bool IsCollidingWithGroup(KinematicCollision2D collision, string group)
    {
        return ((Node)collision.GetCollider()).IsInGroup(group);
    }

    private static bool IsCollidingOnTopOrBottom(KinematicCollision2D collision)
    {
        return collision.GetNormal().Y == 1 || collision.GetNormal().Y == -1;
    }
}
