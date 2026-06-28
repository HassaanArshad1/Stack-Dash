using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private StartScreen startScreen;
    [SerializeField] private HUD hud;
    [SerializeField] private GameOverScreen gameOverScreen;

    private void OnEnable() => GameManager.OnStateChanged += HandleStateChanged;
    private void OnDisable() => GameManager.OnStateChanged -= HandleStateChanged;

    private void HandleStateChanged(GameManager.GameState state)
    {
        startScreen.gameObject.SetActive(state == GameManager.GameState.Menu);
        hud.gameObject.SetActive(state == GameManager.GameState.Playing);
        gameOverScreen.gameObject.SetActive(state == GameManager.GameState.GameOver);
    }
}