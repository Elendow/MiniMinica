using TwitchChat;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Screen loginScreen;
    public Screen gameSelectScreen;

    private IGameController currentGame;

    private void Awake()
    {
        loginScreen.Initialize();
        gameSelectScreen.Initialize();
    }

    private IEnumerator Start()
    {
        EventManager.onGameSelected += OnGameSelected;
        EventManager.onGameCompleted += OnGameComplete;
        TwitchController.onChannelJoined += OnChannelJoined;
        TwitchController.onTwitchMessageReceived += OnTwitchMessageReceived;

        yield return new WaitForSeconds(1f);

        loginScreen.Show();
    }

    private void OnDestroy()
    {
        EventManager.onGameSelected -= OnGameSelected;
        EventManager.onGameCompleted -= OnGameComplete;
        TwitchController.onChannelJoined -= OnChannelJoined;
        TwitchController.onTwitchMessageReceived -= OnTwitchMessageReceived;
    }

    private void OnChannelJoined()
    {
        loginScreen.Hide();
        gameSelectScreen.Show();
    }

    private void OnGameSelected(IGameController game)
    {
        gameSelectScreen.Hide();

        currentGame = game;
        StartCoroutine(currentGame.StartGame());
    }

    private void OnGameComplete()
    {
        currentGame = null;
        gameSelectScreen.Show();
    }

    private void OnTwitchMessageReceived(Chatter chatter)
    {
        if (currentGame == null || !currentGame.IsActive())
        {
            return;
        }

        if (!currentGame.CheckValidMessage(chatter.message))
        {
            return;
        }

        if (currentGame.CheckValue(chatter.message))
        {
            currentGame.CorrectAnswer(chatter);
        }
        else
        {
            currentGame.IncorrectAnswer(chatter);
        }
    }
}
