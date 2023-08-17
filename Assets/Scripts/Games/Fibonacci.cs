using System.Collections;
using TwitchChat;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Fibonacci : GameScreen, IGameController
{
    [Header("Fibonacci")]
    public TextMeshProUGUI valueOne;
    public TextMeshProUGUI valueTwo;

    public Color[] valueColor;

    private int firstValue;
    private int secondValue;

    private int resultValue;
    private int currentColor;

    private int count;

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

        firstValue = secondValue;
        secondValue = resultValue;

        CalculateValue();
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
        valueOne.enabled = false;
        valueTwo.enabled = false;

        Show();

        yield return ShowInstructions();

        message.SetText(string.Empty);

        valueOne.alpha = 1f;
        valueTwo.rectTransform.anchoredPosition = new Vector2(
            100f,
            valueOne.rectTransform.anchoredPosition.y);

        count = 0;
        firstValue = 1;
        secondValue = 1;

        CalculateValue();
        isActive = true;
    }

    private void CalculateValue()
    {
        resultValue = firstValue + secondValue;

        int newColor;
        do
        {
            newColor = Random.Range(0, valueColor.Length);
        } 
        while (newColor == currentColor);

        currentColor = newColor;

        valueOne.enabled = true;
        valueOne.SetText(firstValue.ToString());
        valueOne.color = valueColor[currentColor];
        valueOne.transform.DOShakeScale(0.1f);

        valueTwo.enabled = true;
        valueTwo.SetText(secondValue.ToString());
        valueTwo.color = valueColor[currentColor];
        valueTwo.transform.DOShakeScale(0.1f);

        valueOne.alpha = 1f - (count * 0.2f);
        valueTwo.rectTransform.anchoredPosition = new Vector2(
            Mathf.Clamp(100f - (count * 20f), 0, 100),
            valueOne.rectTransform.anchoredPosition.y);
        count++;
    }
}
