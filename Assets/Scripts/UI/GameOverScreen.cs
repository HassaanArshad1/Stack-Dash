using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
            finalTimeLabel.text = ScoreManager.Instance.GetFormattedTime();
            bestTimeLabel.text = ScoreManager.Instance.GetFormattedBestTime();
        }
    }

    private void OnRestartClicked() =>
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
