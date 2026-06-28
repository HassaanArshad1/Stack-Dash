using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public float CurrentTime { get; private set; }
    public float BestTime { get; private set; }

    private bool _isTiming;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void OnEnable()
    {
        GameManager.OnStateChanged += HandleStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnStateChanged -= HandleStateChanged;
    }

    private void Update()
    {
        if (!_isTiming) return;
        CurrentTime += Time.deltaTime;
    }

    private void HandleStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Playing)
        {
            CurrentTime = 0f;
            _isTiming = true;
        }
        else if (state == GameManager.GameState.GameOver)
        {
            _isTiming = false;
            if (CurrentTime > BestTime)
                BestTime = CurrentTime;
        }
    }

    public string GetFormattedTime() => $"{Mathf.FloorToInt(CurrentTime)}";

    public string GetFormattedBestTime() => $"{Mathf.FloorToInt(BestTime)}";
}