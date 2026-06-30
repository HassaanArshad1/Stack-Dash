using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalTimeLabel;
    [SerializeField] private TextMeshProUGUI bestTimeLabel;
    [SerializeField] private Button restartButton;

    private void OnEnable()
    {
        restartButton.onClick.AddListener(OnRestartClicked);
        SetupScreen();
    }

    private void OnDisable()
    {
        restartButton.onClick.RemoveListener(OnRestartClicked);
    }

    private void SetupScreen()
    {
        bool isGameOver = GameManager.Instance.State == GameManager.GameState.GameOver;
        gameObject.SetActive(isGameOver);

        if (isGameOver)
        {
            finalTimeLabel.text = $"Your Time {ScoreManager.Instance.GetFormattedTime()}";
            bestTimeLabel.text = $"Best Time {ScoreManager.Instance.GetFormattedBestTime()}";
        }
    }

    private void OnRestartClicked()
    {
        GameManager.Instance.RestartGame();
    }
}
