using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class StackCollector : MonoBehaviour
{
    public int StackCount { get; private set; }

    public static event System.Action<int> OnStackChanged;

    private PlayerController _player;

    private void Awake()
    {
        _player = GetComponent<PlayerController>();
    }
    
    private void OnEnable()
    {
        GameManager.OnStateChanged += HandleStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnStateChanged -= HandleStateChanged;
    }
    
    private void HandleStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Menu)
        {
            StackCount = 0;
            OnStackChanged?.Invoke(StackCount);
        }
    }

    public void AddToStack(int amount = 1)
    {
        StackCount += amount;
        OnStackChanged?.Invoke(StackCount);
    }

    public bool ConsumeStack(int amount)
    {
        if (StackCount < amount) return false;
        StackCount -= amount;
        OnStackChanged?.Invoke(StackCount);
        return true;
    }
}
