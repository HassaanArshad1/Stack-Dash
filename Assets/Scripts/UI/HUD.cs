using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerLabel;

    private bool _isActive;

    private void OnEnable() => _isActive = true;
    private void OnDisable() => _isActive = false;
    
    private void Update()
    {
        if (!_isActive) return;
        timerLabel.text = ScoreManager.Instance.GetFormattedTime();
    }
}
