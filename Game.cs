using Godot;

public partial class Game : Node2D
{
    int ScorePlayer1 = 0;
    int ScorePlayer2 = 0;

    public void _OnButtonPressed()
    {
        GetNode<CharacterBody2D>("Ball").Call("Start");
        GetNode<Button>("UI/Button").Visible = false;
        GetNode<CharacterBody2D>("Ball").GlobalPosition = new Vector2(
            GetViewportRect().Size.X / 2,
            GetViewportRect().Size.Y / 2
        );
    }

    public void _OnVisibleOnScreenNotifier2dScreenExited()
    {
        if (GetNode<CharacterBody2D>("Ball").GlobalPosition.X < 0)
        {
            ScorePlayer2++;
            GetNode<Label>("UI/VBoxContainer/LabelPlayer2").Text = ScorePlayer2.ToString();
        }
        else
        {
            ScorePlayer1++;
            GetNode<Label>("UI/VBoxContainer/LabelPlayer1").Text = ScorePlayer1.ToString();
        }
        GetNode<Button>("UI/Button").Visible = true;
    }
}
