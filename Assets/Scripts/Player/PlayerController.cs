using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float forwardSpeed = 8f;
    
    [Header("Lateral Movement")]
    [SerializeField] private float lateralSpeed = 0.05f;
    [SerializeField] private float lateralLimit = 3.5f;

    private InputReader _input;
    private bool _isMoving;
    private Vector3 _startPosition;

    public event System.Action OnTap;

    private void Awake()
    {
        _input = GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _input.OnTap += HandleTap;
        _input.OnMove += HandleMove;
        GameManager.OnStateChanged += HandleStateChanged;
    }

    private void OnDisable()
    {
        _input.OnTap -= HandleTap;
        _input.OnMove -= HandleMove;
        GameManager.OnStateChanged -= HandleStateChanged;
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
        
        if (state == GameManager.GameState.Menu)
            transform.position = _startPosition;
    }
    
    private void HandleMove(Vector2 delta)
    {
        if (GameManager.Instance.State != GameManager.GameState.Playing) return;

        float newX = transform.position.x + delta.x * lateralSpeed;
        newX = Mathf.Clamp(newX, -lateralLimit, lateralLimit);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}