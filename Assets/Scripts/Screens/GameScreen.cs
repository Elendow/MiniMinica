using UnityEngine;
using System.Collections;
using TMPro;
using DG.Tweening;

public class GameScreen : Screen
{
    [Header("Game Screen")]
    public Sprite icon;
    public CanvasGroup instructions;
    public TextMeshProUGUI message;

    protected bool isActive;

    protected IEnumerator ShowInstructions()
    {
        instructions.DOFade(1f, 0.2f);
        yield return new WaitForSeconds(3f);
        instructions.DOFade(0f, 0.2f);
    }

    protected IEnumerator GameOverSequence()
    {
        yield return new WaitForSeconds(0.5f);
        Hide();
        EventManager.onGameCompleted.Invoke();
    }

    public bool IsActive()
    {
        return isActive;
    }
}
