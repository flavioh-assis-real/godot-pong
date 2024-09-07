using Godot;

public partial class Player : CharacterBody2D
{
	const float Speed = 300f;
	const string UpInput = $"UP_1";
	const string DownInput = $"DOWN_1";

	public override void _PhysicsProcess(double delta)
	{
		this.Velocity = GetVelocityFromInput();
		MoveAndCollide(this.Velocity * (float)delta);
	}

	private static Vector2 GetVelocityFromInput()
	{
		if (Input.IsActionPressed(UpInput))
			return new Vector2(0, -Speed);

		if (Input.IsActionPressed(DownInput))
			return new Vector2(0, Speed);

		return new Vector2(0, 0);
	}
}
