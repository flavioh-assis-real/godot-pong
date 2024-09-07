using Godot;

public partial class Ball : CharacterBody2D
{
    [Export]
    int Speed = 200;
    Vector2 direction;

    public override void _Ready()
    {
        this.direction = new Vector2(0, 0);
    }

    public override void _PhysicsProcess(double delta)
    {
        var collision = MoveAndCollide(this.direction * (float)delta);

        if (collision is null) return;

        this.ChangeDirection(collision);
    }

    private static bool IsCollidingInGroup(KinematicCollision2D collision, string group)
    {
        return ((Node)collision.GetCollider()).IsInGroup(group);
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

    private void ChangeDirection(KinematicCollision2D collision)
    {
        var isTouchingPlayer = IsCollidingInGroup(collision, "player");
        var isTouchingWall = IsCollidingInGroup(collision, "walls");

        if (isTouchingPlayer)
        {
            if (collision.GetNormal().Y == 1 || collision.GetNormal().Y == -1)
                this.direction.Y *= -1 * 1.50f;
            else
                this.direction.X *= -1 * 1.50f;

        }
        if (isTouchingWall)
            this.direction.Y *= -1;
    }

    public void Start()
    {
        this.direction = GetInitialDirection();
    }
}
