using Godot;

public partial class Player : CharacterBody2D
{
	[Export]
	int IdPlayer = 1;
	const float Speed = 300f;

	string upInput;
	string downInput;

	public override void _Ready()
	{
		upInput = $"UP_{IdPlayer}";
		downInput = $"DOWN_{IdPlayer}";
	}

	public override void _PhysicsProcess(double delta)
	{
		this.Velocity = GetVelocityFromInput();
		MoveAndCollide(this.Velocity * (float)delta);
	}

	private Vector2 GetVelocityFromInput()
	{
		var ballPosition = GetNode<KinematicCollision2D>("Ball").GlobalPosition;


		if (Input.IsActionPressed(upInput))
			return new Vector2(0, -Speed);

		if (Input.IsActionPressed(downInput))
			return new Vector2(0, Speed);

		return new Vector2(0, 0);
	}
}
