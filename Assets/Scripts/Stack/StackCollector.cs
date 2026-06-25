using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class StackCollector : MonoBehaviour
{
    public int StackCount { get; private set; }

    public event System.Action<int> OnStackChanged;

    private PlayerController _player;

    private void Awake()
    {
        _player = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        _player.OnTap += HandlePlayerTap;
    }

    private void OnDisable()
    {
        _player.OnTap -= HandlePlayerTap;
    }
    
    private void HandlePlayerTap() => AddToStack();

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
