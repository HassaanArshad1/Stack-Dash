using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerLabel;
    [SerializeField] private TextMeshPro stackCountLabel;
    
    private bool _isActive;

    private void OnEnable()
    {
        StackCollector.OnStackChanged += UpdateStackLabel;
        _isActive = true;
    }

    private void OnDisable() => _isActive = false;
    
    private void Start()
    {
        StackCollector.OnStackChanged += UpdateStackLabel;
    }
    
    private void UpdateStackLabel(int count)
    {
        if (count > 8)
        {
            stackCountLabel.text = count.ToString();
        }
        else
        {
            stackCountLabel.text = "";
        }
    }
    
    private void Update()
    {
        if (!_isActive) return;
        timerLabel.text = ScoreManager.Instance.GetFormattedTime();
    }
}
