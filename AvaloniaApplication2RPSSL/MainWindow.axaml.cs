using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaApplication2RPSSL;

public partial class MainWindow : Window
{
    private const int WinningScore = 5;

    private int _humanScore = 0;
    private int _agentScore = 0;
    private bool _gameFinished = false;

    private readonly Random _random = new Random();

    private enum Shape
    {
        Rock,
        Paper,
        Scissors,
        Lizard,
        Spock
    }

    public MainWindow()
    {
        InitializeComponent();
        UpdateUserInterface();
    }

    private void OnShapeClick(object? sender, RoutedEventArgs e)
    {
        if (_gameFinished)
            return;

        if (sender is not Button btn)
            return;

        string tag = btn.Tag?.ToString() ?? "";
        Shape humanShape = Enum.Parse<Shape>(tag);
        Shape agentShape = GetRandomShape();

        LastHumanShapeTextBlock.Text = humanShape.ToString();
        LastAgentShapeTextBlock.Text = agentShape.ToString();

        int result = CompareShapes(humanShape, agentShape);

        if (result == 0)
        {
            RoundResultTextBlock.Text = "Draw!";
        }
        else if (result == 1)
        {
            _humanScore++;
            RoundResultTextBlock.Text = "You win the round!";
        }
        else
        {
            _agentScore++;
            RoundResultTextBlock.Text = "Computer wins the round!";
        }

        CheckWinner();
        UpdateUserInterface();
    }

    private Shape GetRandomShape()
    {
        int value = _random.Next(0, 5);
        return (Shape)value;
    }

    private int CompareShapes(Shape h, Shape a)
    {
        if (h == a)
            return 0;


        return (h, a) switch
        {
            (Shape.Rock, Shape.Scissors) => 1,
            (Shape.Rock, Shape.Lizard) => 1,

            (Shape.Paper, Shape.Rock) => 1,
            (Shape.Paper, Shape.Spock) => 1,

            (Shape.Scissors, Shape.Paper) => 1,
            (Shape.Scissors, Shape.Lizard) => 1,

            (Shape.Lizard, Shape.Spock) => 1,
            (Shape.Lizard, Shape.Paper) => 1,

            (Shape.Spock, Shape.Scissors) => 1,
            (Shape.Spock, Shape.Rock) => 1,

            _ => -1
        };
    }

    private void CheckWinner()
    {
        if (_humanScore >= WinningScore)
        {
            _gameFinished = true;
            WinnerAnnounceTextBlock.Text = "You win the game!";
        }
        else if (_agentScore >= WinningScore)
        {
            _gameFinished = true;
            WinnerAnnounceTextBlock.Text = "Computer wins the game!";
        }
    }

    private void UpdateUserInterface()
    {
        HumanScoreTextBlock.Text = _humanScore.ToString();
        AgentScoreTextBlock.Text = _agentScore.ToString();
    }
}
