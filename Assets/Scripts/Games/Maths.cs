using System.Collections;
using TwitchChat;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Maths : GameScreen, IGameController
{
    public enum Operation { plus, minus, multiply }

    [Header("Maths")]

    public TextMeshProUGUI equation;

    [Space()]

    public Color[] valueColor;

    private int resultValue;
    private int currentColor;

    public bool CheckValidMessage(string message)
    {
        return int.TryParse(message, out int result);
    }

    public bool CheckValue(string message)
    {
        if (int.TryParse(message, out int result))
        {
            return result.Equals(resultValue);
        }

        return false;
    }

    public void CorrectAnswer(Chatter player)
    {
        message.SetText($"Big Brain {player.tags.displayName}!");
        GetRandomValue();
    }

    public void IncorrectAnswer(Chatter player)
    {
        message.SetText($"Small Brain {player.tags.displayName}");
        StartCoroutine(GameOverSequence());
    }

    public IEnumerator StartGame()
    {
        isActive = false;
        message.SetText(string.Empty);
        equation.enabled = false;

        Show();

        yield return ShowInstructions();

        message.SetText(string.Empty);
        GetRandomValue();
        isActive = true;
    }

    private void GetRandomValue()
    {
        int leftValue = Random.Range(-10, 10);
        int rightValue = Random.Range(-1, 10);
        Operation operation = (Operation)Random.Range(0, 3);

        switch (operation)
        {
            case Operation.plus:
                resultValue = leftValue + rightValue;
                break;
            case Operation.minus:
                resultValue = leftValue - rightValue;
                break;
            case Operation.multiply:
                resultValue = leftValue * rightValue;
                break;
        }

        int newColor;
        do
        {
            newColor = Random.Range(0, valueColor.Length);
        }
        while (newColor == currentColor);
        currentColor = newColor;

        equation.enabled = true;
        equation.color = valueColor[currentColor];
        equation.SetText($"{leftValue} {GetSign(operation)} {rightValue}");
        equation.transform.DOShakeScale(0.1f);
    }

    private string GetSign(Operation operation)
    {
        switch (operation)
        {
            case Operation.plus:
                return "+";
            case Operation.minus:
                return "-";
            case Operation.multiply:
                return "x";
        }

        return string.Empty;
    }
}
