using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameSelectScreen : Screen 
{
    public GameScreen[] games;
    public Image gameIcon;

    private GameScreen previousGame;

    public override void Initialize()
    {
        base.Initialize();

        foreach(GameScreen game in games)
        {
            game.Initialize();
        }
    }

    public override void Show()
    {
        base.Show();
        StartCoroutine(Shuffle());
    }

    private IEnumerator Shuffle()
    {
        List<GameScreen> shuffledGames = new List<GameScreen>(games);
        shuffledGames.Shuffle();
        
        if (previousGame != null)
        {
            shuffledGames.Remove(previousGame);
        }

        int i = 0;
        while (i < shuffledGames.Count)
        {
            gameIcon.sprite = shuffledGames[i].icon;
            gameIcon.transform.DOShakeScale(0.1f);

            i++;
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(2f);

        previousGame = shuffledGames[shuffledGames.Count - 1];
        EventManager.onGameSelected.Invoke(shuffledGames[shuffledGames.Count - 1] as IGameController);
    }
}
