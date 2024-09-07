using Godot;

public partial class Game : Node2D
{
    int ScorePlayer1 = 0;
    int ScorePlayer2 = 0;
    bool IsRunning = false;

    public void _OnButtonPressed()
    {
        GetNode<CharacterBody2D>("Ball").Call("Start");
        GetNode<Button>("UI/Button").Visible = false;
        GetNode<CharacterBody2D>("Ball").GlobalPosition = new Vector2(
            GetViewportRect().Size.X / 2,
            GetViewportRect().Size.Y / 2
        );
        this.IsRunning = true;
    }

    public void _OnVisibleOnScreenNotifier2dScreenExited()
    {
        if (GetNode<CharacterBody2D>("Ball").GlobalPosition.X < 0)
        {
            this.ScorePlayer2++;
            GetNode<Label>("UI/VBoxContainer/LabelPlayer2").Text = this.ScorePlayer2.ToString();
        }
        else
        {
            this.ScorePlayer1++;
            GetNode<Label>("UI/VBoxContainer/LabelPlayer1").Text = this.ScorePlayer1.ToString();
        }
        GetNode<Button>("UI/Button").Visible = true;
        IsRunning = false;
    }
}
