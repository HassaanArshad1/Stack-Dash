using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { Menu, Playing, GameOver }
    public GameState State { get; private set; } = GameState.Menu;

    public event System.Action<GameState> OnStateChanged;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void StartGame()  => SetState(GameState.Playing);
    public void TriggerGameOver() => SetState(GameState.GameOver);

    private void SetState(GameState newState)
    {
        State = newState;
        OnStateChanged?.Invoke(State);
    }
}