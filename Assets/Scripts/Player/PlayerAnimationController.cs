using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");

    private void OnEnable() => GameManager.OnStateChanged += HandleStateChanged;
    private void OnDisable() => GameManager.OnStateChanged -= HandleStateChanged;

    private void HandleStateChanged(GameManager.GameState state)
    {
        animator.SetBool(IsRunning, state == GameManager.GameState.Playing);
    }
}
