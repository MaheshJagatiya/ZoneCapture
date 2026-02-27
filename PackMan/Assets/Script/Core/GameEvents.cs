using System;

public static class GameEvents
{
    public static Action OnPlayerDied;
    public static Action<float> OnAreaCaptured;
    public static Action<int> OnLivesChanged;
    public static Action OnPowerUpCollected;

    public static Action OnGameOver;
    public static Action OnLevelComplete;

    public static Action OnGridUpdated;
}
