using UnityEngine.Events;

public static class EventManager
{
    public static UnityAction<IGameController> onGameSelected;
    public static UnityAction onGameCompleted;
}
