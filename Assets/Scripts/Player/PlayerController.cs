using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float forwardSpeed = 8f;

    private InputReader _input;
    private bool _isMoving;

    public event System.Action OnTap;

    private void Awake()
    {
        _input = GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _input.OnTap += HandleTap;
        GameManager.Instance.OnStateChanged += HandleStateChanged;
    }

    private void OnDisable()
    {
        _input.OnTap -= HandleTap;
        GameManager.Instance.OnStateChanged -= HandleStateChanged;
    }

    private void Update()
    {
        if (!_isMoving) return;
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }

    private void HandleTap()
    {
        if (GameManager.Instance.State == GameManager.GameState.Menu)
            GameManager.Instance.StartGame();

        if (GameManager.Instance.State == GameManager.GameState.Playing)
            OnTap?.Invoke();
    }

    private void HandleStateChanged(GameManager.GameState state)
    {
        _isMoving = state == GameManager.GameState.Playing;
    }
}