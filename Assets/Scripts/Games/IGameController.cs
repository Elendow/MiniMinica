using TwitchChat;
using System.Collections;

public interface IGameController
{
    public bool IsActive();
    public IEnumerator StartGame();
    public bool CheckValidMessage(string message);
    public bool CheckValue(string message);
    public void CorrectAnswer(Chatter player);
    public void IncorrectAnswer(Chatter player);
}
