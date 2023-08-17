using System.Collections.Generic;
using System.Collections;
using TwitchChat;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RockPaperScissors : GameScreen, IGameController
{
    private enum RPS { Rock, Paper, Scissors};

    [Header("Rock Paper Scissors")]

    public Image valueImage;

    [Space()]

    public Color[] valueColor;

    [Space()]

    public Sprite rockSprite;
    public Sprite paperSprite;
    public Sprite scissorsSprite;

    [Space()]

    public List<string> RockValidWords;
    public List<string> PaperValidWords;
    public List<string> ScissorsValidWords;

    private RPS currentValue = 0;
    private int currentColor;

    public bool CheckValidMessage(string message)
    {
        if (RockValidWords.Contains(message.ToLower()) ||
            PaperValidWords.Contains(message.ToLower()) ||
            ScissorsValidWords.Contains(message.ToLower()))
        {
            return true;
        }

        return false;
    }

    public bool CheckValue(string message)
    {
        switch (currentValue)
        {
            case RPS.Rock:
                return PaperValidWords.Contains(message.ToLower());
            case RPS.Paper:
                return ScissorsValidWords.Contains(message.ToLower());
            case RPS.Scissors:
                return RockValidWords.Contains(message.ToLower());
        }

        return false;
    }

    public void CorrectAnswer(Chatter player)
    {
        message.SetText($"Good job {player.tags.displayName}!");
        GetRandomValue();
    }

    public void IncorrectAnswer(Chatter player)
    {
        message.SetText($"Shame on {player.tags.displayName}");
        StartCoroutine(GameOverSequence());
    }

    public IEnumerator StartGame()
    {
        isActive = false;
        message.SetText(string.Empty);
        valueImage.enabled = false;

        Show();

        yield return ShowInstructions();

        GetRandomValue();
        isActive = true;
    }

    private void GetRandomValue()
    {
        currentValue = (RPS)Random.Range(0, 3);
        switch (currentValue)
        {
            case RPS.Rock:
                valueImage.sprite = rockSprite;
                break;
            case RPS.Paper:
                valueImage.sprite = paperSprite;
                break;
            case RPS.Scissors:
                valueImage.sprite = scissorsSprite;
                break;
        }

        int newColor;
        do
        {
            newColor = Random.Range(0, valueColor.Length);
        } 
        while (newColor == currentColor);

        currentColor = newColor;
        valueImage.enabled = true;
        valueImage.color = valueColor[currentColor];
        valueImage.transform.DOShakeScale(0.1f);
    }
}
