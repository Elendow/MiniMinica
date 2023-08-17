using System.Collections;
using TwitchChat;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Colors : GameScreen, IGameController
{
    [Header("Colors")]
    public TextMeshProUGUI value;
    public ColorValue[] values;

    private ColorValue currentValue;

    public bool CheckValidMessage(string message)
    {
        foreach (ColorValue value in values)
        {
            if (value.name.Equals(message.ToLower()))
            {
                return true;
            }
        }

        return false;
    }

    public bool CheckValue(string message)
    {
        return currentValue.name.Equals(message.ToLower());
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
        value.text = string.Empty;

        Show();

        yield return ShowInstructions();

        GetRandomValue();
        isActive = true;
    }

    private void GetRandomValue()
    {
        currentValue = values[Random.Range(0, values.Length)];
        value.color = currentValue.color;
        value.text = values[Random.Range(0, values.Length)].name;
        value.transform.DOShakeScale(0.1f);
    }
}

[System.Serializable]
public class ColorValue
{
    public string name;
    public Color color;
}
